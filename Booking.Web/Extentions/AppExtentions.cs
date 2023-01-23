using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Extentions
{
    public static class AppExtentions
    {
        public static IList<SelectListItem> SetSelectedValue(this IList<SelectListItem> collection, int? id)
        {
            if (id != null)
            {
                var item = collection.FirstOrDefault(item => item.Value != null && item.Value.Equals(id.ToString()));
                if (item is not null)
                    item.Selected = true;
            }
            
            return collection;
        }

        public static IList<SelectListItem> AddAllItem(this IList<SelectListItem> collection, bool itemAllSelected = true)
        {
            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = itemAllSelected };
            collection.Insert(0, allItem);
            return collection;
        }
    }
}
