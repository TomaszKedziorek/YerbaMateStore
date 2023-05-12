function AddToCart(url) {
  $.ajax({
    url: url,
    type: 'POST',
    success: function (data) {
      if (data.success) {
        toastr.success(data.message);
        $("#TotalPriceForProductsInCart").html(data.totalPriceForProductsInCart);
        $("#AllProductsInCartCount").html(data.allProductsInCartCount);
      }
      else {
        toastr.error(data.message);
      }
    },
    error:function () {
      toastr.warning("Please log in first!");
    }
  })
}