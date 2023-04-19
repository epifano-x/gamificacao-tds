

namespace GerenRest.RazorPages.Data {
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {

            
            //context.AddRange(events);
            context.SaveChanges();
        }
    }
}