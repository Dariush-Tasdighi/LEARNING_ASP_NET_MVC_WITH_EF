using System.Linq;
using System.Data.Entity;

namespace MY_MVC_APPLICATION.Controllers
{
	public partial class UsersController : Infrastructure.BaseController
	{
		public UsersController()
			: base()
		{
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			var varUsers =
				MyDatabaseContext.Users
				.Include(current => current.Role)
				.OrderBy(current => current.FullName)
				.ToList()
				;

			return (View(model: varUsers));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Create()
		{
			Models.User oDefaultUser = new Models.User();

			oDefaultUser.Age = 20;
			oDefaultUser.IsActive = true;

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: null);
			// **************************************************

			return (View(model: oDefaultUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "Id,RoleId,IsActive,Age,Username,Password,FullName,Description")] Models.User user)
		{
			if ((user.Age == 30) && (user.FullName.Length == 5))
			{
				// Note: [null] is invalid value!
				ModelState.AddModelError(key: "", errorMessage: "Some error message!");

				//ModelState.AddModelError(key: "FullName", errorMessage: "Some error message!");
			}

			if (ModelState.IsValid)
			{
				MyDatabaseContext.Users.Add(user);

				MyDatabaseContext.SaveChanges();

				return (RedirectToAction(MVC.Users.Index()));
			}

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			//ViewBag.RoleId =
			//	new System.Web.Mvc.SelectList
			//		(items: varRoles, dataValueField: "Id", dataTextField: "Name");

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: user.RoleId);
			// **************************************************

			return (View(model: user));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: oUser.RoleId);
			// **************************************************

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,RoleId,IsActive,Age,Username,Password,FullName,Description")] Models.User user)
		{
			if (ModelState.IsValid)
			{
				MyDatabaseContext.Entry(user).State = EntityState.Modified;

				MyDatabaseContext.SaveChanges();

				return (RedirectToAction(MVC.Users.Index()));
			}

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: user.RoleId);
			// **************************************************

			return (View(model: user));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			MyDatabaseContext.Users.Remove(oUser);

			MyDatabaseContext.SaveChanges();

			return (RedirectToAction(MVC.Users.Index()));
		}
	}
}
