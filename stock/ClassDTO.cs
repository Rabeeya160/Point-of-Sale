using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace stock
{
    class ClassDTO

    {
        public string categoryName { get; set; }
        public string product_name { get; set; }

        public string employee_name { get; set; }

        public string supplier_name { get; set; }

        public string product_number { get; set; }

        public string product_number_issue { get; set; }



        public static ArrayList category_name = new ArrayList();
        public static ArrayList name_product = new ArrayList();
        public static ArrayList name_employee = new ArrayList();
        public static ArrayList name_supplier = new ArrayList();
        public static ArrayList number_product = new ArrayList();
        public static ArrayList number_product_issue = new ArrayList();
        public ClassDTO()
        {
        }

        public static void AddCatregory(ClassDTO ddl)
        {
            category_name.Add(ddl);
        }

        public static void AddIssueNumber(ClassDTO ddl)
        {
            number_product_issue.Add(ddl);
        }

        public static void AddSuppplier(ClassDTO ddl)
        {
            name_supplier.Add(ddl);
        }

        public static void addproduct(ClassDTO ddl)
        {
            name_product.Add(ddl);
        }

        public static void Addnumber(ClassDTO ddl)
        {
            number_product.Add(ddl);
        }




        public static void Product_number(ClassDTO ddl)
        {
            for (int i =0; i< 5; i++)
            {
              
                 
            }
        } 

        public static void employeename(ClassDTO ddl)
        {
            name_employee.Add(ddl);
        }
    }

    }

