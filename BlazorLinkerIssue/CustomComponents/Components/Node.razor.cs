using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomComponents.Components
{
    public partial class Node : ComponentBase
    {
        [Parameter]
        public bool RenderNext { get; set; } = true;
    }
}
