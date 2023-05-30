/*  Crestron Masters 2023 MSS-421 class Javascript for CWS REST integration
 *  Written by Tim Gray Sr Technical Trainer for CTI
 *  This code is for educational purposes only.  Do not call true blue support for help with this code
 */

// Define some constants that point at static elements on the webpage by leveraging the element ID
// This is useful if you are going to access these elements in several functions below.

// NOTE READ THIS! 

// Something that is critical to know that is not documented.    VC-4 serves up HTML from https://IPADDRESS/VirtualControl/Rooms/ROOMID/Html/
// This is NOT where CWS resides and is different from a 3 series or 4 series processor, this can cause  difficulty getting a CWS
// Web interface working on a VC-4 Instance that you had working on a processor.
// what we need to do is append the path on VC-4 by telling  javascript,  "back up one level" by adding a .. 
// ../cws/app/settings will get back to where CWS created it's hooks int othe web server.
// Be very aware of this when writing portable code, you will need to write javascript to detect VC-4 (parse the http path) and adjust your REST
// path accordingly to be correct for your web pages.

let nameInput = document.getElementById('MastersClass');
let locationInput = document.getElementById('IPAddress');
let helpNumberInput = document.getElementById('Port');
let UiPasswordInput = document.getElementById('password');


let saveBtn = document.getElementById('save-changes');
saveBtn.addEventListener('click', (e) => {
    putSettings();
});
let reloadBtn = document.getElementById('reload');
reloadBtn.addEventListener('click', (e) => {
    location.reload();
});


async function handleDelete(EndPointName) {
    NvxObject = {
        Address: '0.0.0.0',  //we really do not care about the ip address as we are going to delete based on the name
        Name: EndPointName
    }

    let result = await fetch('../cws/app/settings/del', { 
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(NvxObject)
    });

        location.reload();
     
}

async function handleAdd()
{
    NvxObject = {
        Address: document.getElementById("address-new").value,
        Name: document.getElementById("name-new").value
    }

    let result = await fetch('../cws/app/settings/add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(NvxObject)
    });

        location.reload();
     
}

// This is where we save everything from the webpage back to the processor
async function putSettings () {
    newSettings = {
        MastersClass: nameInput.value,
        IPAddress: locationInput.value,
        Port: helpNumberInput.value,
        UiPassword: UiPasswordInput.value,
        Endpoints: []
    }
    var names = new Array();
    var ips = new Array();
    count = 0;
    /*   
     *   We now need to find all the text input fields on the webpage and read their contents
     *   Because we have TWO of them for every entry AND we have unique ID's for them
     *   we need to use a wildcard capable selector.  We then put all of them into the 
     *   Object array with a direct push formatting it properly.
     */
        Array.from(document.querySelectorAll('[id^=endName]')).forEach((item) => {
        names.push(item.value);
        count++;
    });

    Array.from(document.querySelectorAll('[id^=endIp]')).forEach((item) => {
        ips.push(item.value);
    });

    for( i = 0; i <= count;  i++) {
        if(names[i] || ips[i])  // Make sure we are not saving an empty entry
        newSettings.Endpoints.push({Address: ips[i], Name: names[i]});
    }

    // Send it to the processor for saving the data
    let result = await fetch('../cws/app/settings/all', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newSettings)
    });
    location.reload();
}



// Get the settings from the processor's CWS server we set up cws/server/route/DATA
async function getSettings () {
    let result = await fetch('../cws/app/settings/all');
    let data = await result.json();
    // we now extract that information and spit it into the webpage
    // the information we are after is now inside the object data
    // to get the information we use data.ELEMENTNAME
    nameInput.value = data.MastersClass;
    locationInput.value = data.IPAddress;
    helpNumberInput.value = data.Port;
    UiPasswordInput.value = data.UiPassword;

    i = 1;
    // we are now going to iterate through all of the endpoints and create html code for each of them 
    // This will build out the list of endpoints for display and editing
    data.Endpoints.forEach((item) => {
        let div = document.createElement('div');
        
        let label = document.createElement('label');
        label.setAttribute('for', `endpoint-${i}`);
        label.innerText = `Source ${i}: `;
        div.appendChild(label);
        
        div.appendChild(document.createTextNode(' ')); // Just stuffing in a space for formatting

        let textField = document.createElement('input');
        textField.setAttribute('type', 'text');
        textField.setAttribute('id', `endName-${i}`);
        textField.setAttribute('class', `names`);
        textField.setAttribute('value', item.Name);
        div.appendChild(textField);

        let ipField = document.createElement('input');
        ipField.setAttribute('type', 'text');
        ipField.setAttribute('id', `endIp-${i}`);
        textField.setAttribute('class', `ips`);
        ipField.setAttribute('value', item.Address);
        div.appendChild(ipField);

        div.appendChild(document.createTextNode(' ')); // Just stuffing in a space for formatting

        // Add a delete button
        let delbtn = document.createElement('button');
        delbtn.setAttribute('id', `delete-${i}`);
        delbtn.setAttribute('onClick', "handleDelete(\"" + item.Name + "\")");
        delbtn.innerHTML = " Delete ";
       
        div.appendChild(delbtn);

        document.getElementById('Endpoints').appendChild(div); // This right here is where it tells it to put Everything we just did at the bottom of the DIV called "endpoints"
        i += 1;
    });

    // Add In a blank endpoint entry at the end of all of them. this preserves alignment instead of static html

    let div = document.createElement('div');
        
    let label = document.createElement('label');
    label.setAttribute('for', `input-new`);
    label.innerText = `Add New : `;
    div.appendChild(label);
    
    div.appendChild(document.createTextNode(' ')); // Just stuffing in a space for formatting

    let textField = document.createElement('input');
    textField.setAttribute('type', 'text');
    textField.setAttribute('id', `name-new`);
    textField.setAttribute('value', '');
    div.appendChild(textField);

    let ipField = document.createElement('input');
    ipField.setAttribute('type', 'text');
    ipField.setAttribute('id', `address-new`);
    ipField.setAttribute('value', '');
    div.appendChild(ipField);

    div.appendChild(document.createTextNode(' ')); // Just stuffing in a space for formatting

    // Add a save button.
    let addbtn = document.createElement('button');
    addbtn.setAttribute('id', `add-new`);
    addbtn.setAttribute('onClick', "handleAdd()");
    addbtn.innerHTML = " Add to Endpoints ";
   
    div.appendChild(addbtn);

    document.getElementById('Endpoints').appendChild(div);  // Again stuffing everything above we created at the bottom of the div set called "Endpoints"


    // This will color the border of any text box changed to red to let us know it changed.
    Array.from(document.getElementsByTagName('input')).forEach((item) => {
        item.addEventListener('change', () => {
            item.style.borderColor = 'red';
            saveBtn.style.visibility = 'visible';
        });
    });
}

setTimeout(getSettings, 100);