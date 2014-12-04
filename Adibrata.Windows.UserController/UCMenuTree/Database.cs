using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adibrata.Windows.UserController
{
    public static class Database
    {

        #region GetFamilyTree

        public static Person GetFamilyTree()
        {

            // In a real app this method would access a database.
            return new Person
            {
                Name = "Main Menu",
                Children =
                {
                    new Person
                    {
                        Name="Agreement",
                        Children=
                        {
                            new Person
                            {
                                Name="Customer",
                                Children=
                                {
                                    new Person
                                    {
                                        Name="Customer"
                                    },
                                    new Person
                                    {
                                        Name="Add Customer"
                                    }
                                }
                            },
                            new Person
                            {
                                Name="Agreement Inquiry"
                            }
                        }
                    },
                    new Person
                    {
                        Name="Upload",
                        Children=
                        {
                           
                            new Person
                            {
                                Name="Upload Processing"
          
                            },
                             new Person
                            {
                                Name="Upload Monitoring",
                                Children=
                                {
                                    new Person
                                    {
                                        Name="Local Inquiry"
                                    },
                                    new Person
                                    {
                                        Name="Server Inquiry"
                                    }
                                }
                            }
                        }
                    },
                                        new Person
                    {
                        Name="Support",
                        Children=
                        {
                           
                            new Person
                            {
                                Name="Video Call Support"
          
                            },
                                                        new Person
                            {
                                Name="Email Support"
          
                            }
                        }
                    }
                }
            };
        }

        #endregion // GetFamilyTree
    }
}
