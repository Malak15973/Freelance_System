let registerCloseBtn = document.querySelector(".RegisterForm .exit_sign");
let registerPopForm = document.getElementById("registerForm");
let signUpBtn = document.querySelector(".navbar .collapse .SignUp");

let loginCloseBtn = document.querySelector(".LoginForm .exit_sign");
let loginPopForm = document.getElementById("loginForm");
let loginBtn = document.querySelector(".navbar .collapse .Login");

registerCloseBtn.addEventListener("click", () => {
  registerPopForm.classList.add("d-none");
});

signUpBtn.addEventListener("click", () => {
  registerPopForm.classList.remove("d-none");
});

loginBtn.addEventListener("click", () => {
  loginPopForm.classList.remove("d-none");
});

loginCloseBtn.addEventListener("click", () => {
  loginPopForm.classList.add("d-none");
});
