﻿const userName = "nik";
const password = "1234567";

window.addEventListener("DOMContentLoaded", () => {
    const controlDiv = document.getElementById("controls");

    createButton(controlDiv, "Get Data", getData);
    createButton(controlDiv, "Log In", login);
    createButton(controlDiv, "Log Out", logout);
});

async function login() {
    let response = await fetch("/api/account/login", {
        method: "post",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            username: userName,
            password: password
        })
    });

    if (response.ok) {
        displayData("Logged in");
    } else {
        displayData(`Error: ${response.status}: ${response.statusText}`);
    }
}

async function logout() {
    let response = await fetch("/api/account/logout", {
        method: "post"
    });

    if (response.ok) {
        displayData("Logged out");
    } else {
        displayData(`Error: ${response.status}: ${response.statusText}`);
    }
}

async function getData() {
    let response = await fetch("/api/people");
    if (response.ok) {
        let jsonData = await response.json();

        displayData(...jsonData.map(item => `${item.surname}, ${item.firstname}`));
    } else {
        displayData(`Error: ${response.status}: ${response.statusText}`);
    }
}

function displayData(...items) {
    const dataDiv = document.getElementById("data");
    dataDiv.innerHTML = "";

    items.forEach(i => {
        const itemDiv = document.createElement("div");
        itemDiv.innerText = i;
        itemDiv.style.wordWrap = "break-word";

        dataDiv.appendChild(itemDiv);
    });
}

function createButton(parent, label, handler) {
    const button = document.createElement("button");
    button.classList.add("btn", "btn-primary", "m-2");
    button.innerText = label;
    button.onclick = handler;

    parent.appendChild(button);
}