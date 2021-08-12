using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace testapp2.Classes
{
 public  class ChatTemplateSelector:DataTemplateSelector
    {
     public   DataTemplate IncomingTemplate { get; set; }
     public   DataTemplate OutcomingTemplate { get; set; }

       

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messagetype = item as ChatViewModel;
                if (messagetype != null)
             {
                  return (messagetype.Reciver == "no") ? OutcomingTemplate : IncomingTemplate;
             }
             else
                 return null;
           
        }
    }
}
