namespace MovieStore.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MovieStore.Controllers;
    using MovieStore.Models;
    using System.Web.Mvc;
    using System.Web.WebPages;

    [TestClass]
    public class MovieStoreControllerTest
    {
        [TestMethod]
        public void MovieStore_Index_TestView()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_ListOfMovies()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            //Act
            List<Movie> result = controller.ListOfMovies();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Iron Man 1", actual: result[0].Title);
            Assert.AreEqual(expected: "Iron Man 2", actual: result[1].Title);
            Assert.AreEqual(expected: "Iron Man 3", actual: result[2].Title);
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_Success()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            //Act
            RedirectToRouteResult result = controller.IndexRedirect(1) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Create", actual: result.RouteValues["action"]);
            Assert.AreEqual(expected: "HomeController", actual: result.RouteValues["controller"]);
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_BadRequest()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            //Act
            HttpStatusCodeResult result = controller.IndexRedirect(0) as HttpStatusCodeResult;


            //Assert
            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_ListFromDb()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.ListFromDb() as ViewResult;
            List<Movie> resultMovies = result.Model as List<Movie>;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_Details_Success()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.Details(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieStore_Details_IdIsNull()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_Details_MovieIsNull()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            HttpStatusCodeResult result = controller.Details(0) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(expected: HttpStatusCode.NotFound, actual: (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_Create_Get_TestView()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            //Act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }


 //unsure how to create [HttpPost] unit it test for Create
 //I could not figure out how to test for model state validity, or produce a view with a movie object
        //[TestMethod]
        //public void MovieStore_Create_PostView()
        //{
        //    //Goal: Query from our own list instead of the database
        //    //Step 1
        //    var list = new List<Movie>
        //    {
        //        new Movie() { MovieId = 1, Title = "Movie 1"},
        //        new Movie() {MovieId = 2, Title = "Movie 2"},
        //    }.AsQueryable();

        //    //Step 2
        //    Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
        //    Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

        //    //Step 3
        //    mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
        //    mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
        //    mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
        //    mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

        //    //Step 4
        //    mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

        //    // Arrange
        //    MoviesController controller = new MoviesController(mockContext.Object);

        //    //Act
        //    ViewResult result = controller.Create() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Arrange
        //    //MoviesController controller = new MoviesController();

        //    //Act
        //   // ViewResult result = controller.Create() as ViewResult;

        //    //Assert
        //    //Assert.IsNotNull(result);
        //}





        [TestMethod]
        public void MovieStore_Edit_Get_Success()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Movie 1"},
                new Movie() {MovieId = 2, Title = "Movie 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.Edit(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void MovieStore_Edit_Get_IdIsNull()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            HttpStatusCodeResult result = controller.Edit(id: null) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);
        }
        [TestMethod]
        public void MovieStore_Edit__Get_MovieIsNull()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            HttpStatusCodeResult result = controller.Edit(0) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(expected: HttpStatusCode.NotFound, actual: (HttpStatusCode)result.StatusCode);
        }
 
 //unsure how to create [HttpPost] unit it test for Edit
 //my attempt for this function is below
        //I could not figure out how to test for model state validity
        //[TestMethod]
        // public void MovieStore_Edit_Post_ModelStateValidation()
        //{
        //var list = new List<Movie>
        //{
        //  new Movie() { MovieId = 1, Title = "Superman 1"},
        //   new Movie() {MovieId = 2, Title = "Superman 2"},
        // }.AsQueryable();
        //Step 2
        // Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
        //Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();


        //Step 3
        // mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
        // mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
        //mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
        //mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

        //Step 4
        //mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

        // Arrange
        // MoviesController controller = new MoviesController(mockContext.Object);
        //Act
        // RedirectToRouteResult result = controller.Edit(Movie movie) as RedirectToRouteResult;

        //Assert
        //Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void MovieStore_Delete_Get_Success()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Movie 1"},
                new Movie() {MovieId = 2, Title = "Movie 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            ViewResult result = controller.Delete(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void MovieStore_Delete_Get_IdIsNull()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            HttpStatusCodeResult result = controller.Delete(id: null) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);
        }
        [TestMethod]
        public void MovieStore_Delete__Get_MovieIsNull()
        {
            //Goal: Query from our own list instead of the database
            //Step 1
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            HttpStatusCodeResult result = controller.Delete(0) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(expected: HttpStatusCode.NotFound, actual: (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void MovieStore_DeleteConfirmed_Success()
        {
            var list = new List<Movie>
            {
                new Movie() { MovieId = 1, Title = "Movie 1"},
                new Movie() {MovieId = 2, Title = "Movie 2"},
            }.AsQueryable();

            //Step 2
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            //Step 3
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());


            //Step 4
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            //Act
            RedirectToRouteResult result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Index", actual: result.RouteValues["action"]);
        } // unsure how to build unit test code for the following: db.Movies.Remove(movie); db.SaveChanges();
    }


}

