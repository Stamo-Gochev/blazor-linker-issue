using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomComponents.Components
{
    public partial class NodeFragment : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
