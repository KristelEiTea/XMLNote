using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckAndLoad();
            List<Note> Notes = new List<Note>();

            Notes = new List<Note>();
            var Serializer = new XmlSerializer(typeof(List<Note>));
            using (var Reader = XmlReader.Create("../../Notes.xml"))
            {
                Notes = (List<Note>)Serializer.Deserialize(Reader);
            }

            while (true)
            {
                Console.WriteLine("Kas te soovite faili lisada(\"lisa\"), vaadata (\"vaata\") või kustutada(\"kustuta\")? Lahkumiseks sisestage \"exit\".");
                string teha1 = Console.ReadLine();

                if (teha1 == "lisa")
                {
                    while (true)
                    {
                        Console.WriteLine("Sisestage märkme nimetus");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Sisestage märkme sisu");
                        string Content = Console.ReadLine();

                        if (Name != "" && Content != "")
                        {
                            Notes.Add(new Note() { Name = Name, Content = Content });
                            using (var Writer = XmlWriter.Create("../../Notes.xml"))
                            {
                                Serializer.Serialize(Writer, Notes);
                                Console.Clear();
                                Console.WriteLine("Note on lisatud!");
                                break;
                            }
                        }
                    }
                }

                else if (teha1 == "vaata")
                {
                    if (Notes.Count == 0)
                    {
                        while (true)
                        {
                            Console.WriteLine("Teil pole märkmeid");
                            Console.WriteLine("Soovite jätkata Notepad9009 kasutusega? (\"jah\"/\"ei\")");
                            string teha3 = Console.ReadLine();

                            if (teha3 == "jah")
                            {
                                Console.Clear();
                                break;
                            }
                            else if (teha3 == "ei")
                            {
                                Environment.Exit(1);
                            }
                        }
                    }

                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Notes: ");
                            int i = 0;
                            foreach (var item in Notes)
                            {
                                i++;
                                Console.WriteLine(i + ") " + item.Name);
                            }
                            Console.WriteLine("Sisestage number avamiseks:");
                            int vaata = int.Parse(Console.ReadLine());
                            if (vaata < 1 | vaata > Notes.Count)
                            {
                                Console.Clear();
                                Console.WriteLine("Uh-oh, midagi läks valesti!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Märkme: " + "\"" + Notes[vaata - 1].Name + "\"" + " sisu:\n" + Notes[vaata - 1].Content);
                                break;
                            }
                        }
                    }
                }

                else if (teha1 == "kustuta")
                {
                    if (Notes.Count == 0)
                    {
                        while (true)
                        {
                            Console.WriteLine("Teil pole märkmeid");
                            Console.WriteLine("Soovite jätkata Notepad9009 kasutusega? (\"jah\"/\"ei\")");
                            string teha3 = Console.ReadLine();

                            if (teha3 == "jah")
                            {
                                Console.Clear();
                                break;
                            }
                            else if (teha3 == "ei")
                            {
                                Environment.Exit(1);
                            }
                        }
                    }

                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Notes: ");
                            int i = 0;
                            foreach (var item in Notes)
                            {
                                i++;
                                Console.WriteLine(i + ") " + item.Name);
                            }
                            Console.WriteLine("Sisestage number kustutamiseks:");
                            int kustuta = int.Parse(Console.ReadLine());
                            if (kustuta < 1 | kustuta > Notes.Count)
                            {
                                Console.Clear();
                                Console.WriteLine("Uh-oh, midagi läks valesti!");
                            }
                            else
                            {
                                Notes.RemoveAt(kustuta - 1);
                                using (var Writer = XmlWriter.Create("../../Notes.xml"))
                                {
                                    Serializer.Serialize(Writer, Notes);
                                    Console.Clear();
                                    Console.WriteLine("Note kustutatud!");
                                    break;
                                }
                            }
                        }
                    }
                }

                else if (teha1 == "exit")
                {
                    Environment.Exit(1);
                }

                else continue;
            }
        }

        static void CheckAndLoad()
        {
            List<Note> Notes = new List<Note>();
            XmlSerializer Serializer = new XmlSerializer(typeof(List<Note>));

            if (!(File.Exists("../../Notes.xml")))
            {
                using (var Writer = XmlWriter.Create("../../Notes.xml"))
                {
                    Serializer.Serialize(Writer, Notes);
                }
            }
        }
    }

    public class Note
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}



