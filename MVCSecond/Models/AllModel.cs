using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCSecond.Models
{
   


    public class AllModel
    {
     

        [Display(Name ="Անուն")]
       public string Name { set; get; }

        [Display(Name = "Ազգանուն")]
        public string Surname { set; get; }

        [Display(Name = "Ավանդի տեսակ")]
        public string AvandName { set; get; }

        [Display(Name = "Տոկոս")]
        public int Percent { set; get; }


        [Display(Name = "Բանկի անվանում")]
        public string BankName { set; get; }

        [Display(Name = "Oր")]
        public DateTime? order_date { set; get; }



        [Display(Name = "Գումար")]
        public int? Price { set; get; }

       
        //Bank
        [Display(Name = "Բանկեր")]
        public string Bank { set; get; }
        [Display(Name = "Ինֆո")]
        public string Info { set; get; }

    }

}