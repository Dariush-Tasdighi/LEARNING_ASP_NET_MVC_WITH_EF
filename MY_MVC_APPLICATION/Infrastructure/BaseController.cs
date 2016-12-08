namespace Infrastructure
{
	public class BaseController : System.Web.Mvc.Controller
	{
		public BaseController()
			: base()
		{
			// Solution (2)
			//MyDatabaseContext =
			//	new Models.DatabaseContext();
			// /Solution (2)
		}

		// Note: Wrong Usage!
		//private Models.DatabaseContext _myDatabaseContext = new Models.DatabaseContext();

		// Note: Not Recommended
		//public Models.DatabaseContext MyDatabaseContext = new Models.DatabaseContext();

		// Solution (1)
		//protected Models.DatabaseContext MyDatabaseContext = new Models.DatabaseContext();
		// /Solution (1)

		// Solution (2)
		//protected Models.DatabaseContext MyDatabaseContext;
		// /Solution (2)

		// Solution (3)
		//protected Models.DatabaseContext MyDatabaseContext;
		// /Solution (3)

		// Solution (4)
		private Models.DatabaseContext _myDatabaseContext;

		/// <summary>
		/// Lazy Loading = Lazy Initialization
		/// </summary>
		protected Models.DatabaseContext MyDatabaseContext
		{
			get
			{
				if (_myDatabaseContext == null)
				{
					_myDatabaseContext =
						new Models.DatabaseContext();
				}

				return (_myDatabaseContext);
			}
		}
		// /Solution (4)

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_myDatabaseContext != null)
				{
					_myDatabaseContext.Dispose();
					_myDatabaseContext = null;
				}
			}

			base.Dispose(disposing);
		}
	}
}
