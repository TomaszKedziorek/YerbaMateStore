# YerbaMateStore
This is a test online store.
It was created using ASP.NET MVC, Entity Framework and free template: [Zay Shop Template](https://templatemo.com/tm-559-zay-shop).

Other third-party components are:
- [DataTable](https://datatables.net/)
- [toastr notifications](https://github.com/CodeSeven/toastr)
- [sweetalert2 popup](https://sweetalert2.github.io/)
- [slick carousel](https://kenwheeler.github.io/slick/)
- [Stripe payments](https://stripe.com/)

## Functionalities
The YerbaMateStore application allows to make fictitious purchases three types of products: yerba mate, bombilla (straw with strainer) and cup for drinking.

To perform such ficious purchases one need to create an account, confirm email by clicking in the link sent from the store and log in.
After adding the product to the basket, the user can place an order as in any other online store by filling in the delivery data, choosing the delivery and payment method, placing the order and paying for the order if the payment method was chosen by online transfer.

At this point, the role of the Individual User (see the Account section) ends. The Admin or Employee should then start processing the order, cancel it or indicate that the dummy order was - equally dummy - shipped. If the order has been canceled and the order was paid by online transfer, this fictitious money will be automatically refunded.

## Accounts
The application possess three types of accounts: Individual, Admin and Employee. By default, anyone can create Individual account. The other two types: Admin and Employee accounts, are possible to create only for an Admin. By default, the apllication create one Admin account durin first launch, during which also default products are created in database.
The Administrator or Employee can also create new, edit or delete existing products.

All users can delete their account with all their shared informations, except the  last Admin user.

Every actions related to the user's account (e.g. registery, log in , forgt password), exept sending emails (performed by MimeKit), are managed by Microsoft AspNetCore Identity, appropriately adapted to this particular application.

## Payment and Delivery
The application supports two types of payment: online transfer and offline payment (e.g. payment on pickup). All payment methods are linked to the delivery method. Both are managed by an Admin or an Employee.
Yerba Mate Store application uses Stripe Online Payment, so to testing payment one need to use testing cards data - see [Stripe Testing](https://stripe.com/docs/testing)

By default Stripe Payments are disabled, but the application in this state pretends payment success or failure, and user may choose one these two.

To Enabled Stripe Payments go to /YerbaMateStore/Models/Utilities/StaticDetails.cs and change StripePaymentEnabled to true.

