using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_awinder7.Models
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private BookstoreContext context;
        public EFCheckoutRepository(BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<CheckoutBag> Checkout => context.CheckoutBags.Include(x => x.Lines).ThenInclude(x => x.Book);

        public void SaveCheckout(CheckoutBag checkout)
        {
            context.AttachRange(checkout.Lines.Select(x => x.Book));
            if (checkout.BagId == 0)
            {
                context.CheckoutBags.Add(checkout);
            }
            context.SaveChanges();
        }
    }
}
