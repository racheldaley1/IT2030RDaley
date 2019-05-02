using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApplication.Models
{
    public class TicketsOrdered
    {
        public string TicketsOrderedId;

        private EventDB db = new EventDB();

        public static TicketsOrdered GetOrder(HttpContextBase context)
        {
            TicketsOrdered order = new TicketsOrdered();
            order.TicketsOrderedId = order.GetOrderId(context);
            return order;
        }

        private string GetOrderId(HttpContextBase context)
        {
            const string OrderSessionId = "OrderId";

            string orderId;

            if (context.Session[OrderSessionId] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    orderId = context.User.Identity.Name;
                    
                }

                else
                {
                    orderId = Guid.NewGuid().ToString();
                }
                context.Session[OrderSessionId] = orderId;
                //Create a new cart id
                //orderId = Guid.NewGuid().ToString();

                //Save to the session date.
                //context.Session[OrderSessionId] = orderId;
            }
            else
            {
                //Return the existing cart id
                orderId = context.Session[OrderSessionId].ToString();
            }

            return orderId;
        }

        public List<Order> GetOrderItems()
        {
            return db.Orders.Where(c => c.OrderId == this.TicketsOrderedId).ToList();
        }

        public void AddToOrder(int eventId)
        {
            //TODO: Verify that the Album Id exists in the database.
            Order orderItem = db.Orders.SingleOrDefault(c => c.OrderId == TicketsOrderedId && c.EventId == eventId);

            if (orderItem == null)
            {
                Event order = db.Events.SingleOrDefault(a => a.EventId == eventId);
                Random random = new Random();
                int randomOrderId = random.Next(1, 999999);
                // Item is not in cart; add new cart item
                orderItem = new Order()
                {
                    OrderId = TicketsOrderedId,
                    EventId = eventId,
                    EventSelected = order,
                    TicketCount = 1,
                    DateCreated = DateTime.Now,
                    OrderStatus = "Processed",
                    OrderNumber = randomOrderId
                };
                db.Orders.Add(orderItem);
            }
            else
            {
                // Item is already; increase item count(quantity)
                orderItem.TicketCount++;
            }

            db.SaveChanges();
        }

        public int RemoveFromOrder(int recordId)
        {
            Order orderItem = db.Orders.SingleOrDefault(c => c.OrderId == this.TicketsOrderedId && c.RecordId == recordId);

            if (orderItem == null)
            {
                throw new NullReferenceException();
            }

            int newTicketCount;

            if (orderItem.TicketCount > 1)
            {
                //The count>1; decrement count
                orderItem.TicketCount--;
                newTicketCount = orderItem.TicketCount;
            }
            else
            {
                //The count <=0; remove cart item
                db.Orders.Remove(orderItem);
                newTicketCount = 0;
                orderItem.OrderStatus = "Cancelled";
            }

            db.SaveChanges();

            return newTicketCount;
        }

    }
}