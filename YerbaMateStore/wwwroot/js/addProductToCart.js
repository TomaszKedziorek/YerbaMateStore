function AddToCart(url) {
  $.ajax({
    url: url,
    type: 'POST',
    success: function (data) {
      if (data.success) {
        toastr.success(data.message);
      }
      else {
        toastr.error(data.message);
      }
    }
  })
}