﻿using BookWiki.Shared.Data.DB;
using BookWiki_Console;

var BookDAL = new DAL<Book>(new BookWikiContext());

Dictionary<string, Book> BookDict = new();

bool exit = false;
while (!exit)
{
    Console.WriteLine("Você chegou na BOOKWIKI!\n");
    Console.WriteLine("Digite 1 para registrar um livro");
    Console.WriteLine("Digite 2 para registrar um autor de um livro");
    Console.WriteLine("Digite 3 para mostrar todos os livros");
    Console.WriteLine("Digite 4 para mostrar os autores de um livro");
    Console.WriteLine("Digite -1 para sair\n");

    Console.Write("Digite sua opção: ");
    int option = int.Parse(Console.ReadLine());

    switch (option)
    {
        case 1:
            BookRegister();
            break;
        case 2:
            AuthorRegister();
            break;
        case 3:
            BookGet();
            break;
        case 4:
            AuthorGet();
            break;
        case -1:
            Console.WriteLine("Tchau, obrigado!");
            exit = true;
            break;
        default:
            Console.WriteLine("Escolha inválida");
            break;
    }
    Thread.Sleep(1500);
    Console.Clear();
}

void AuthorGet()
{
    Console.Clear();
    Console.WriteLine("Listagem de Autor\n");
    Console.Write("Digite o livro cujo autor você quer listar: ");
    string bookName = Console.ReadLine();
    var targetBook = BookDAL.ReadBy(a => a.Title.Equals(bookName));
    if (targetBook is not null)
    {
        targetBook.Authors = new List<Author>();
        foreach(var author in targetBook.Authors)
        {
            Console.WriteLine(author);
        }
    }
    else Console.WriteLine($"Livro {bookName} não encontrado");
}

void BookGet()
{
    Console.Clear();
    Console.WriteLine("Listagem de Livros\n");
    foreach (var book in BookDAL.Read())
    {
        Console.WriteLine(book);
    }
}

void AuthorRegister()
{
    Console.Clear();
    Console.WriteLine("Registro de Autor\n");
    Console.Write("Digite o livro cujo autor você quer registrar: ");
    string bookName = Console.ReadLine();
    var targetBook = BookDAL.ReadBy(a=> a.Title.Equals(bookName));
    if (targetBook is not null)
    {
        Console.Write($"Qual o nome do autor de {bookName}? ");
        string authorName = Console.ReadLine();
        targetBook.AddAuthor(new Author(authorName));
        BookDAL.Update(targetBook);
        Console.WriteLine($"Autor {authorName} de {bookName} adicionado!");
    }
    else Console.WriteLine($"Livro {bookName} não encontrado");
}

void BookRegister()
{
    Console.Clear();
    Console.WriteLine("Registro de Livros\n");
    Console.Write("Digite o nome do livro: ");
    string title = Console.ReadLine();
    Console.Write("Digite o sumário do livro: ");
    string summary = Console.ReadLine();
    Console.Write("Digite o ano de lançamento do livro: ");
    string publicationYear = Console.ReadLine();
    Book book = new Book(title, summary, Convert.ToInt32(publicationYear));
    BookDAL.Create(book);
    Console.WriteLine($"Livro {title} adicionado!");
}