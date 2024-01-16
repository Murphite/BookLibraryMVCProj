// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function loginOpen() {
    const login = document.getElementById("login");
    const overlay = document.getElementById("overlay");
    login.style.display = "block";
    overlay.style.display = "block";
}
function loginOut() {
    const login = document.getElementById("login");
    const overlay = document.getElementById("overlay");
    login.style.display = "none";
    overlay.style.display = "none";
}

function showConfirmationPopup(button) {
    // Get the book ID from the data-book-id attribute
    var bookId = button.getAttribute('data-book-id');

    // Set the href attribute of the "Yes" button with the correct book ID
    document.getElementById('yesButton').href = "/Book/Delete/" + bookId;

    // Show the confirmation popup
    document.getElementById("login").style.display = "block";
}



let addRoleBtn = document.querySelector("#add-role-btn");
let addUserRoleBtn = document.querySelector("#add-user-role-btn");
let addRolePanel = document.querySelector("#new-role-panel");
let newUserRolePanel = document.querySelector("#new-user-role-panel");


/*console.log(delBtns)*/

if (addRoleBtn != null) {
    addRoleBtn.addEventListener('click', () => {
        addRolePanel.classList.remove("hide");
        addRolePanel.classList.add("show");
    })
}

if (addUserRoleBtn != null) {

    addUserRoleBtn.addEventListener('click', () => {
        newUserRolePanel.classList.remove("hide");
        newUserRolePanel.classList.add("show")
    })
}