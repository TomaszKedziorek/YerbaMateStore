const togglePassword = document.querySelector('#eyeIconPassword');
const password = document.querySelector('#inputPassword');

const toggleConfirm = document.querySelector('#eyeIconConfirm');
const confirm = document.querySelector('#inputConfirm');

const toggleOldPassword = document.querySelector('#eyeIconOldPassword');
const oldPassword = document.querySelector('#inputOldPassword');

$(document).ready(function () {
  showHidePassword(togglePassword, password);
  showHidePassword(toggleConfirm, confirm);
  showHidePassword(toggleOldPassword, oldPassword);
})

function showHidePassword(toodleIcon, element) {
  if (toodleIcon) {
    toodleIcon.addEventListener('click', function () {
      const type = element.getAttribute('type');
      if (type === 'password') element.setAttribute('type', 'text');
      else element.setAttribute('type', 'password');
      this.classList.toggle('bi-eye');
    });
  }
}