const togglePassword = document.querySelector('#eyeIconPassword');
const toggleConfirm = document.querySelector('#eyeIconConfirm');
const password = document.querySelector('#inputPassword');
const confirm = document.querySelector('#inputConfirm');
const wewe = document.querySelector('#qwqwqwq');

$(document).ready(function () {
  showHidePassword(togglePassword, password);
  showHidePassword(toggleConfirm, confirm);
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