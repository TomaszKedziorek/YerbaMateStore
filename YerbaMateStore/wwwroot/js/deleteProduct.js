function Delete(url) {
  Swal.fire({
    title: 'Are you sure?',
    text: "You won't be able to revert this!",
    icon: 'warning',
    iconColor: '#e74c3c',
    showCancelButton: true,
    confirmButtonColor: "#375a7f",
    cancelButtonColor: '#e74c3c',
    confirmButtonText: 'Yes, delete it!'
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
          if (data.success) {
            dataTable.ajax.reload();
            toastr.success(data.message);
          }
          else {
            toastr.error(data.message);
          }
        }
      })
    }
  })
}