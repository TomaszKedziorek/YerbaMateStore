using Stripe.Checkout;
using YerbaMateStore.Models.Entities;
using YerbaMateStore.Models.Utilities;

namespace YerbaMateStore.Models.Managers;
public class StripePaymentManager<T> where T : IShoppingCartOrOrderDetail
{
  private OrderHeader orderHeader { get; set; }
  private IEnumerable<T> YerbaMateCartOrOrderDetailList { get; set; }
  private IEnumerable<T> BombillaCartOrOrderDetailList { get; set; }
  private IEnumerable<T> CupCartOrOrderDetailList { get; set; }

  public StripePaymentManager(OrderHeader orderHeader, IEnumerable<T> yerbaMateCartOrOrderDetailList, IEnumerable<T> bombillaCartOrOrderDetailList, IEnumerable<T> cupCartOrOrderDetailList)
  {
    this.orderHeader = orderHeader;
    YerbaMateCartOrOrderDetailList = yerbaMateCartOrOrderDetailList;
    BombillaCartOrOrderDetailList = bombillaCartOrOrderDetailList;
    CupCartOrOrderDetailList = cupCartOrOrderDetailList;
  }

  public Session StripePayment(string successUrl, string cancelUrl)
  {
    SessionCreateOptions options = StripePaymentOptions(successUrl, cancelUrl);
    SessionService service = StripePaymentService();
    Session session = service.Create(options);
    return session;
  }

  public static Session GetSessionById(string sessionId)
  {
    SessionService service = StripePaymentService();
    Session session = service.Get(sessionId);
    return session;
  }

  private static SessionService StripePaymentService()
  {
    SessionService service = new SessionService();
    return service;
  }

  private SessionCreateOptions StripePaymentOptions(string successUrl, string cancelUrl)
  {
    //Stripe payment settings
    var domain = StaticDetails.Domain;
    var options = new SessionCreateOptions
    {
      PaymentMethodTypes = new List<string>
        {
          "card",
        },
      LineItems = new List<SessionLineItemOptions>(),
      Mode = "payment",
      SuccessUrl = domain + successUrl,
      CancelUrl = domain + cancelUrl,
    };
    options.LineItems = AddCartItemsToStripeOptionsLineItems();
    return options;
  }

  private List<SessionLineItemOptions> AddCartItemsToStripeOptionsLineItems()
  {
    List<SessionLineItemOptions> LineItems = new List<SessionLineItemOptions>();
    CreateStripeSessionLineItemOptions(ref LineItems, YerbaMateCartOrOrderDetailList, "Brand", "Name");
    CreateStripeSessionLineItemOptions(ref LineItems, BombillaCartOrOrderDetailList, "Name", "Length");
    CreateStripeSessionLineItemOptions(ref LineItems, CupCartOrOrderDetailList, "Name", "Volume");
    CreateStripeSessionLineItemOptionsForOrderDelivery(ref LineItems);
    return LineItems;
  }

  private void CreateStripeSessionLineItemOptions(ref List<SessionLineItemOptions> LineItems, IEnumerable<T> CartOrOrderDetailList, string firstProperty, string srcondProperty)
  {
    foreach (var item in CartOrOrderDetailList)
    {
      var Product = item.GetType().GetProperty("Product").GetValue(item);
      var FirstProperty = Product.GetType().GetProperty(firstProperty).GetValue(Product);
      var SecondProperty = Product.GetType().GetProperty(srcondProperty).GetValue(Product);
      var sessionLineItem = new SessionLineItemOptions
      {
        PriceData = new SessionLineItemPriceDataOptions
        {
          UnitAmount = (int)(item.Price * 100),//this value is int, represent decimal price 20.00->2000
          Currency = StaticDetails.Currency.ToLower(),
          ProductData = new SessionLineItemPriceDataProductDataOptions
          {
            Name = $"{FirstProperty} {SecondProperty}",
          }
        },
        Quantity = item.Quantity
      };
      LineItems.Add(sessionLineItem);
    }
  }

  private void CreateStripeSessionLineItemOptionsForOrderDelivery(ref List<SessionLineItemOptions> LineItems)
  {
    double orderHeaderDeliveryMethodCost = orderHeader.DeliveryMethod.Cost;
    if (orderHeaderDeliveryMethodCost > 0)
    {
      var sessionLineItem = new SessionLineItemOptions
      {
        PriceData = new SessionLineItemPriceDataOptions
        {
          UnitAmount = (int)(orderHeaderDeliveryMethodCost * 100),//this value is int, represent decimal price 20.00->2000
          Currency = StaticDetails.Currency.ToLower(),
          ProductData = new SessionLineItemPriceDataProductDataOptions
          {
            Name = $"Delivery:  {orderHeader.DeliveryMethod.Carrier}",
          }
        },
        Quantity = 1
      };
      LineItems.Add(sessionLineItem);
    }
  }

}
