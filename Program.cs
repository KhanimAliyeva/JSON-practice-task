using System.Text.Json;

namespace WorkingWithJSON;

internal class Program
{
    public static List<User> users;
    public static List<Post> posts;

    static void Main(string[] args)
    {


        DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
        string path = directoryInfo.Parent.Parent.Parent.FullName;

        /*        using (StreamWriter w = new StreamWriter(Path.Combine(path, "users.json")))
                {

                }

                using (StreamWriter w = new StreamWriter(Path.Combine(path, "posts.json")))
                {

                }*/


        using (StreamReader r = new StreamReader(Path.Combine(path, "users.json")))
        {
            string json = r.ReadToEnd();
            users = JsonSerializer.Deserialize<List<User>>(json);
        }

        using (StreamReader r = new StreamReader(Path.Combine(path, "posts.json")))
        {
            string json = r.ReadToEnd();
            posts = JsonSerializer.Deserialize<List<Post>>(json);
        }






        int choice;
    repeat:

        Console.WriteLine("\n--- Menu ---");
        Console.WriteLine("1. List all users");
        Console.WriteLine("2. List all posts");
        Console.WriteLine("3. Show posts by user");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your choice: ");
        choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                ListUsers();
                Console.ReadKey();
                goto repeat;
            case 2:
                ListPosts();
                Console.ReadKey();
                goto repeat;
            case 3:
                ShowPostsByUser();
                Console.ReadKey();
                goto repeat;
            case 4:
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }


    }


    static void ListUsers()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n--- Users ---");
        Console.ResetColor();
        foreach (var user in users)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nID: ");
            Console.ResetColor();
            Console.WriteLine($"{user.id} \nName: {user.name} \nUsername: {user.username} \nEmail: {user.email}");
        }
    }

    static void ListPosts()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n--- Posts ---");
        Console.ResetColor();
        foreach (var post in posts)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nID: ");
            Console.ResetColor();
            Console.WriteLine($"{post.id} \nUserID: {post.userId} \nTitle: {post.title} \nBody: \n{post.body}");
        }
    }

    static void ShowPostsByUser()
    {
        Console.Write("Enter User ID: ");
        int userId = int.Parse(Console.ReadLine());
        var userPosts = posts.Where(p => p.userId == userId).ToList();
        if (userPosts.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No posts found for this user.");
            Console.ResetColor();
            return;
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n--- Posts by User ID {userId} ---");
        Console.ResetColor();
        foreach (var post in userPosts)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nID: ");
            Console.ResetColor();
            Console.WriteLine($"{post.id} \nTitle: {post.title} \nBody: \n{post.body}");
        }
    }


}

