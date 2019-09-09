"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/AmpHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Initialize your app
var myApp = new Framework7({
    animateNavBackIcon: true
});

// Export selectors engine
var $$ = Dom7;

var SourcePicker;

// Add main View
var mainView = myApp.addView('.view-main', {
    // Enable dynamic Navbar
    dynamicNavbar: true,
    // Enable Dom Cache so we can use all inline pages
    domCache: true
});

jQuery.support.cors = true;
$.getScript("http://" + window.location.host + "/" + 'signalr/hubs', InitializeSignalr);

var chat;

var CurrentZoneIndex;
var AllZones;

var AllSources;
var FilteredSources

function InitializeSignalr()
{
    CurrentZoneIndex = 0;

    $.connection.hub.url = "http://" + window.location.host + "/" + "signalr";

    chat = $.connection.myHub

    //
    // Set an event handler for received changes
    //
    chat.client.valueChanged = function (e) {
        //
        // If the Zone that has been updated is the currently selected zone on the page
        // then update the view to match the new data
        //
        if (e.Channel == AllZones[CurrentZoneIndex])
            UpdateView(e.Keypad);
    };

    //
    // Connect to the signalr hub, once connected get the amp and zone state
    //
    $.connection.hub.start().done(function () {
        GetAmplifierState();
        var LastZone = GetCookie("Zone");
        GetZoneState(LastZone);
    });

    //
    // Attach to the disconnected event for the signalr hub
    // Reconnect if the connection is lost
    //
    $.connection.hub.disconnected(function () {
        setTimeout(function () {
            $.connection.hub.start().done(function () {
                GetAmplifierState();
                var LastZone = GetCookie("Zone");
                GetZoneState(LastZone);
            }, 5000)
        })
    });
}

function GetAmplifierState() {
    chat.server.getAmpState().done(function (data) {
        //
        // Loop through all zones to determine which are in use or not
        //
        AllZones = [];

        for (i = 0; i < data.KeypadCount; i++) {
            chat.server.getKeyPad(i).done(function (data) {
                //
                // If the name is null that means that we have manually specified this zone as
                // not used in the amp configuration app
                //
                if (data.Name != "null") {
                    //
                    // By doing a modulus 10 on the ID, we get the zone for that amplifier
                    // So, for example, 11 is zone 1 of amp 1, 12 is zone 2 of amp 1, etc
                    // Because amp 2 and 3 are referenced as 7-12 and 13-18 (I think,
                    // I don't have multiple amps to test with). Then subtract one as the
                    // indexes are 0 based. So 11 is zone 1 of amp 1 but is represented as 0
                    //
                    var UncorrectedID = (data.ID % 10) - 1;

                    //Unit 1
                    if ((data.ID + "")[0] == "1") {
                        AllZones.push(UncorrectedID);
                    }
                    //Unit 2
                    else if ((data.ID + "")[0] == "2") {
                        AllZones.push(UncorrectedID + 6);
                    }
                    //Unit 3
                    else {
                        AllZones.push(UncorrectedID + 12);
                    }

                    //
                    // Because these callbacks are asyncronous, they could execute out or order
                    // so be sure to sort the valid zones
                    //
                    AllZones.sort();
                }
            });
        }

        //
        // Build the source list
        //
        AllSources = data.Sources;

        //
        // Filtered sources will be used to hide unused sources from display
        //
        FilteredSources = [];

        //
        // Sources that are "null" are unused, remove them from the list to add to the drop down selector
        // We keep a separate list for index lookup, as the amp needs 0, 1, 2, etc and not the source name
        //
        for (i = 0; i < AllSources.length; i++) {
            if (AllSources[i] != "null") {
                FilteredSources.push(AllSources[i]);
            }
        }

        SourcePicker = myApp.picker(
            {
                input: '#SourcePicker',
                cols: [
                    {
                        textAlign: 'center',
                        values: FilteredSources,
                        onChange: function (picker, values) {
                            //
                            // We offset the index by 1 because the amplifier starts counting at 1
                            // while JavaScript arrays have a 0 based index
                            //
                            var SourceChannel = AllSources.indexOf(values) + 1;
                            chat.server.setProperty(AllZones[CurrentZoneIndex], "CH", SourceChannel).done(function (data) {
                                GetZoneState(AllZones[CurrentZoneIndex]);
                            });
                        }
                    }]
            });
    })
}

//
// Gets the state of the specified zone from the amplifier/service
// Sets the cookie so that the next time we load the page, we are on the same zone
// and calls a function to update the view
//
function GetZoneState(Zone) {
    chat.server.getKeyPad(Zone).done(function (data) {
        SetCookie("Zone", Zone, 30);
        UpdateView(data);
    })
}

//
// Updates the page to match the currently selected zone
//
function UpdateView(data) {
    //
    // Set the power and mute checkboxes based on the current state of the zone
    //
    $$('#PowerCheckbox').prop('checked', data.PR);
    $$('#MuteCheckbox').prop('checked', data.MU);

    //
    // Because settings cannot be changed while a zone is powered off,
    // disable all controls when power is off or enable when on
    //
    if (data.PR) {
        $$('.DisableOnPowerOff').removeClass('disabled');
    }
    else {
        $$('.DisableOnPowerOff').addClass('disabled');
    }

    //
    // Get the currently selected source for this zone and update
    // the source dropdown to match
    // We subtract 1 because the amplifier/controller starts at 1 but
    // JavaScript arrays are 0 based
    //
    var ChannelSelected = AllSources[data.CH - 1];
    SourcePicker.setValue([ChannelSelected], 0);

    //
    // Set the volume display and slider
    //
    $("#VolumeDisplay").html(data.VO);
    $$('#VolumeSlider').val(data.VO);

    //
    // Set the zone display
    //
    $("#ZoneName").html(data.Name);
}

function SetCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function GetCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');

    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();

        if (c.indexOf(name) == 0)
            return c.substring(name.length, c.length);
    }

    return "";
}

$$('#MuteCheckbox').on('change', function (e) {
    var MuteState = $$('#MuteCheckbox').prop('checked') ? 1 : 0;

    chat.server.setProperty(AllZones[CurrentZoneIndex], "MU", MuteState).done(function (data) {
        GetZoneState(AllZones[CurrentZoneIndex]);
    });
});

$$('#PowerCheckbox').on('change', function (e) {
    var PowerState = $$('#PowerCheckbox').prop('checked') ? 1 : 0;

    chat.server.setProperty(AllZones[CurrentZoneIndex], "PR", PowerState).done(function (data) {
        GetZoneState(AllZones[CurrentZoneIndex]);
    });
});

$("#ZoneNext").click(function (event) {
    CurrentZoneIndex++;

    if (CurrentZoneIndex >= AllZones.length)
        CurrentZoneIndex = 0;

    GetZoneState(AllZones[CurrentZoneIndex]);
});

$("#ZonePrev").click(function (event) {
    CurrentZoneIndex--;

    if (CurrentZoneIndex < 0)
        CurrentZoneIndex = AllZones.length - 1;

    GetZoneState(AllZones[CurrentZoneIndex]);
});

$("#VolumeUp").click(function (event) {
    chat.server.propertyUp(AllZones[CurrentZoneIndex], "VO").done(function (data) {
        GetZoneState(AllZones[CurrentZoneIndex]);
    });
});

$("#VolumeDown").click(function (event) {
    chat.server.propertyDn(AllZones[CurrentZoneIndex], "VO").done(function (data) {
        GetZoneState(AllZones[CurrentZoneIndex]);
    });
});

$$('#VolumeSlider').on('input change', function () {
    chat.server.setProperty(AllZones[CurrentZoneIndex], "VO", this.value).done(function (data) {
        GetZoneState(AllZones[CurrentZoneIndex]);
    });
});
