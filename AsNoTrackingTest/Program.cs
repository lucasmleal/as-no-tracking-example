using AsNoTrackingTest;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


var gc0 = GC.CollectionCount(0);
var gc1 = GC.CollectionCount(1);
var gc2 = GC.CollectionCount(2);

var stopWatch = new Stopwatch();
stopWatch.Start();

Parallel.For(0, 1000, index =>
{
    using var db = new Context();

    var list1 = db.Blogs
        .AsNoTracking()
        .ToList();

    var list2 = db.Posts
        .AsNoTracking()
        .ToList();
});

stopWatch.Stop();

Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds} ms");
Console.WriteLine($"GC0: {GC.CollectionCount(0) - gc0}");
Console.WriteLine($"GC1: {GC.CollectionCount(1) - gc1}");
Console.WriteLine($"GC2: {GC.CollectionCount(2) - gc2}");

Console.Read();



/* for (int i = 0; i < 1000; i++)
{
    db.Add(new Blog { Url = "http://blogs.msdn.com/adonet", Posts = new List<Post> { new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" } } });
    db.SaveChanges();

    var blog = db.Blogs
        .OrderBy(b => b.BlogId)
        .Last();

    Console.WriteLine("Updating the blog and adding a post");
    for (int j = 0; j < 10; j++)
    {
        blog.Posts.Add(
        new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        db.SaveChanges();
    }
}*/

