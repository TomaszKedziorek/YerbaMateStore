namespace YerbaMateStore.Models.Utilities;

public static class StaticDetails
{
  public const string Domain = "https://localhost:7026/";
  public const string Role_Individual = "Individual";
  public const string Role_Admin = "Admin";
  public const string Role_Employee = "Employee";

  public const string StatusPending = "Pending";
  public const string StatusApproved = "Approved";
  public const string StatusInProcess = "Processing";
  public const string StatusShipped = "Shipped";
  public const string StatusCancelled = "Cancelled";
  public const string StatusRefunded = "Refunded";

  public const string PaymentStatusPending = "Pending";
  public const string PaymentStatusApproved = "Approved";
  public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
  public const string PaymentStatusRejected = "Rejected";

  public const string PaymentTypeTransfer = "Transefr";
  public const string PaymentTypeOnPickup = "OnPickup";
}