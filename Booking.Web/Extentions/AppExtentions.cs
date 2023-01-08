using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Extentions
{
    public static class AppExtentions
    {
        public static List<SelectListItem> SetSelectedValue(this List<SelectListItem> collection, int? id)
        {
            var item = collection.FirstOrDefault(item => item.Value.Equals(id.ToString()));
            if (item is not null)
                item.Selected = true;
            return collection;
        }
    }
}
