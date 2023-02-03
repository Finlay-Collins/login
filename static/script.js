let elements = {
    username: document.getElementById("username"),
    password: document.getElementById("password"),
    hints: document.getElementById("hints")
};

function createUser() {

};

function reset() {

};

function login() {
    presenceCheck(elements.password.value, "Please enter a password");
    presenceCheck(elements.username.value, "Please enter a username");
};

function presenceCheck(input, message) {
    if (input == "") {
        elements.hints.innerText = message;
        setTimeout(() => { elements.hints.innerText = ""; }, 3000);
    }

    else {
        elements.hints.innerText = "";
    }
};