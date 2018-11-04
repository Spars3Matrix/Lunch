using System;
using System.Linq;
using Lunch.Commands;
using Lunch.Order;

namespace Lunch.Cli
{
    class SlackSimulator
    {
        private const string ORDER_COMMAND = "/order";
        private const string LIST_ORDER_COMMAND = "/list-order";
        private const string LIST_MENU_COMMAND = "/menu";

        private string Initiator { get; set; }

        private ICommandHandler Handler = new CommandHandler();

        public void PrintWelcomeMessage()
        {
            string message = "Slack Simulator\n";
            message += "\nCommands:";
            message += "\n/order [amount] waldcorn carpaccio";
            message += "\n/list-order [all]";
            message += "\n/menu";
            message += "\n\n";

            Console.Write(message);
        }

        public void SetInitiator()
        {
            Console.Write("Initiator: ");
            Initiator = Console.ReadLine();
        }

        public void ExcecuteCommand()
        {
            Console.Write("Command: "); 
            string command = Console.ReadLine();

            string comm = command.Split(' ')[0];
            string text = command.Substring(comm.Length);
            switch(comm)
            {
                case ORDER_COMMAND:
                    OrderResult result = Handler.Order(Initiator, text);
                    Console.WriteLine(result.Successful ?
                        $"You've ordered a '{result.OrderItem.Description}'." :
                        $"Could not order this item. ({result.Exception.ToString()})");
                    break;
                case LIST_ORDER_COMMAND:
                    Console.WriteLine(string.Join("\n", Handler.ListOrder(Initiator, text).Select(i => $"{i.Amount} x {i.Description} for {i.Price} = {i.Total} ({i.Person})")));
                    break;
                case LIST_MENU_COMMAND:
                    Console.WriteLine(string.Join("\n", Handler.ListMenu(Initiator, text).Select(i => $"{i.Description}: {i.Price} euro")));
                    break;
                default:
                    Console.WriteLine("Command not found");
                    break;
            }

            Console.WriteLine();
        }
    }
}