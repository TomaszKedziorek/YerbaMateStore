function validateInput() {
  if (document.getElementById("trackingNumber").value == "") {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'Please enter Tracking Number!',
      confirmButtonColor: "#375a7f",
      cancelButtonColor: '#e74c3c',
      confirmButtonText: 'Ok',
      showCancelButton: true,
      cancelButtonColor: '#e74c3c'
    });
    return false;
  }
  if (document.getElementById("carrier").value == "") {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'Please enter Carrier!',
      confirmButtonColor: "#375a7f",
      cancelButtonColor: '#e74c3c',
      confirmButtonText: 'Ok',
      showCancelButton: true,
      cancelButtonColor: '#e74c3c'
    });
    return false;
  }
  return true;
}