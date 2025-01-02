using Microsoft.EntityFrameworkCore;
using Labb_2_Databaser.Models;


var optionsBuilder = new DbContextOptionsBuilder<CarlKennedalLabbEttContext>();
optionsBuilder.UseSqlServer("Data Source=MANGLISH;Initial Catalog=CarlKennedalLabbEtt;Integrated Security=True;Trust Server Certificate=True");

using (var context = new CarlKennedalLabbEttContext(optionsBuilder.Options))
{
    while (true)
    {
        Console.WriteLine("Bookstore Management Console");
        Console.WriteLine("1. List Stock Balances");
        Console.WriteLine("2. Add Book to Store");
        Console.WriteLine("3. Remove Book from Store");
        Console.WriteLine("4. View Titles per Author");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ListStockBalances(context);
                break;
            case "2":
                AddBookToStore(context);
                break;
            case "3":
                RemoveBookFromStore(context);
                break;
            case "4":
                DisplayTitlesPerAuthor(context);
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }
}
void ListStockBalances(CarlKennedalLabbEttContext context)
{
    var stockBalances = from sb in context.StockBalances
                        join book in context.Books on sb.ISBN equals book.ISBN
                        join store in context.Stores on sb.StoreId equals store.StoreId
                        select new
                        {
                            StoreName = store.StoreName,
                            BookTitle = book.Title,
                            Quantity = sb.Quantity,
                            Isbn = book.ISBN
                        };

    foreach (var stock in stockBalances)
    {
        Console.WriteLine($"Store: {stock.StoreName}, Book: {stock.BookTitle}, Quantity: {stock.Quantity}. ISBN: {stock.Isbn}");
    }
}
void AddBookToStore(CarlKennedalLabbEttContext context)
{
    Console.Write("Enter Store ID: ");
    var storeId = int.Parse(Console.ReadLine() ?? "0");

    var books = context.Books.ToList();
    Console.WriteLine("Available Books:");
    for (int i = 0; i < books.Count; i++)
    {
        Console.WriteLine($"{i + 1}. Title: {books[i].Title}, ISBN: {books[i].ISBN}");
    }

    Console.Write("Choose a book by number: ");
    var bookChoice = int.Parse(Console.ReadLine() ?? "0");

    if (bookChoice < 1 || bookChoice > books.Count)
    {
        Console.WriteLine("Invalid choice.");
        return;
    }

    var selectedBook = books[bookChoice - 1];

    Console.Write("Enter Quantity: ");
    var quantity = int.Parse(Console.ReadLine() ?? "0");

    var existingStockBalance = context.StockBalances
        .FirstOrDefault(sb => sb.StoreId == storeId && sb.ISBN == selectedBook.ISBN);

    if (existingStockBalance != null)
    {

        var stockBalance = new StockBalance
        {
            StoreId = storeId,
            ISBN = selectedBook.ISBN,
            Quantity = quantity
        };

        existingStockBalance.Quantity += quantity;
        Console.WriteLine($"Updated the stock for '{selectedBook.Title}' in Store ID {storeId}. New quantity: {existingStockBalance.Quantity}");
    }
    else
    {
        var newStockBalance = new StockBalance
        {
            StoreId = storeId,
            ISBN = selectedBook.ISBN,
            Quantity = quantity
        };
        context.StockBalances.Add(newStockBalance);
        Console.WriteLine($"Added {quantity} copies of '{selectedBook.Title}' to Store ID {storeId}.");
    }
    context.SaveChanges();

    Console.WriteLine($"Added {quantity} copies of '{selectedBook.Title}' to Store ID {storeId}.");
}
void RemoveBookFromStore(CarlKennedalLabbEttContext context)
{
    Console.Write("Enter Store ID: ");
    var storeId = int.Parse(Console.ReadLine() ?? "0");

    var stockBalances = context.StockBalances
        .Where(sb => sb.StoreId == storeId)
        .Join(context.Books,
            stock => stock.ISBN,
            book => book.ISBN,
            (stock, book) => new { stock, book })
        .ToList();

    if (!stockBalances.Any())
    {
        Console.WriteLine("No books found in the selected store.");
        return;
    }

    Console.WriteLine("Books in Store:");
    for (int i = 0; i < stockBalances.Count; i++)
    {
        Console.WriteLine($"{i + 1}. Title: {stockBalances[i].book.Title}, ISBN: {stockBalances[i].book.ISBN}, Quantity: {stockBalances[i].stock.Quantity}");
    }

    Console.Write("Choose a book by number to remove: ");
    var bookChoice = int.Parse(Console.ReadLine() ?? "0");

    if (bookChoice < 1 || bookChoice > stockBalances.Count)
    {
        Console.WriteLine("Invalid choice.");
        return;
    }

    var selectedStock = stockBalances[bookChoice - 1].stock;

    Console.Write($"Enter the quantity to remove (current stock: {selectedStock.Quantity}): ");
    var quantityToRemove = int.Parse(Console.ReadLine() ?? "0");

    if (quantityToRemove <= 0)
    {
        Console.WriteLine("Invalid quantity.");
        return;
    }

    if (quantityToRemove > selectedStock.Quantity)
    {
        Console.WriteLine("Cannot remove more than the available stock.");
        return;
    }
    Console.WriteLine($"Are you sure you want to remove {quantityToRemove} copies of '{stockBalances[bookChoice - 1].book.Title}' from Store ID {storeId}? (y/n)");
    var confirmation = Console.ReadLine()?.ToLower();

    if (confirmation != "y")
    {
        Console.WriteLine("Operation canceled.");
        return;
    }
    selectedStock.Quantity -= quantityToRemove;
    if (selectedStock.Quantity == 0)
    {
        context.StockBalances.Remove(selectedStock);
    }

    context.SaveChanges();

    Console.WriteLine($"Removed {quantityToRemove} copies of '{stockBalances[bookChoice - 1].book.Title}' from Store ID {storeId}. Remaining quantity: {selectedStock.Quantity}");
}

static void DisplayTitlesPerAuthor(CarlKennedalLabbEttContext context)
{
    var titlesPerAuthor = context.TitlesPerAuthors.ToList();

    if (!titlesPerAuthor.Any())
    {
        Console.WriteLine("No data available in the TitlesPerAuthor view.");
        return;
    }

    Console.WriteLine("Titles Per Author:");
    Console.WriteLine("--------------------------------");
    foreach (var author in titlesPerAuthor)
    {
        Console.WriteLine($"Author: {author.Name}, Number of Titles: {author.Titles}, Stock Value: {author.StockValue}");
    }
    Console.WriteLine("--------------------------------");
}
