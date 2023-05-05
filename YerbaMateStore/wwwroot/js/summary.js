const OrderDeliveryTotal = document.querySelector('#OrderDeliveryTotal');
const OrderDeliveryTotalInput = document.querySelector('#OrderDeliveryTotalInput');
function AddDeliveryCostToOrderTotal(deliveryCost, orderTotal) {
  let orderAndDeliveryTotal = deliveryCost + orderTotal;
  OrderDeliveryTotal.innerHTML = (orderAndDeliveryTotal).toFixed(2) + " zł";
  OrderDeliveryTotalInput.value = orderAndDeliveryTotal;
}