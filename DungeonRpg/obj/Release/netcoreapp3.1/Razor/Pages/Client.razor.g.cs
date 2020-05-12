#pragma checksum "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ba6957096725e57665f270f303820c3ad5736dd"
// <auto-generated/>
#pragma warning disable 1591
namespace DungeonRpg.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using DungeonRpg;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using DungeonRpg.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using DungeonRpg.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using System.Timers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using static DungeonRpg.Engine.Map;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/client")]
    public partial class Client : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "client");
            __builder.AddMarkupContent(2, "\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "panel-top");
            __builder.AddMarkupContent(5, "\r\n        ");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "panel-left");
            __builder.AddMarkupContent(8, "\r\n            ");
            __builder.OpenElement(9, "div");
            __builder.AddAttribute(10, "class", "panel-map");
            __builder.AddMarkupContent(11, "\r\n                ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "map");
            __builder.AddMarkupContent(14, "\r\n");
#nullable restore
#line 20 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                     if (Map != null)
                    {
                        for (int y = -10; y < 10; y++)
                        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(15, "                            ");
            __builder.OpenElement(16, "div");
            __builder.AddAttribute(17, "class", "map-row");
            __builder.AddMarkupContent(18, "\r\n");
#nullable restore
#line 25 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                 for (int x = -10; x < 10; x++)
                                {
                                    (int x, int y) position = (x + Player.Position.X, y + Player.Position.Y);

#line default
#line hidden
#nullable disable
            __builder.AddContent(19, "                                    ");
            __builder.OpenElement(20, "div");
            __builder.AddAttribute(21, "class", "map-tile");
            __builder.AddMarkupContent(22, "\r\n");
#nullable restore
#line 29 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                         foreach (var layer in Map.LayersEnumerable())
                                        {
                                            var tileId = Map[layer, position.x, position.y];
                                            if (tileId == 4839)
                                            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(23, "                                                ");
            __builder.AddMarkupContent(24, "<div class=\"map-tile-layer\">\r\n                                                    <div class=\"tile tile-68\"></div>\r\n                                                </div>\r\n");
#nullable restore
#line 37 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                            }
                                            else if (tileId != 0)
                                            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(25, "                                                ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "map-tile-layer");
            __builder.AddMarkupContent(28, "\r\n                                                    ");
            __builder.OpenElement(29, "div");
            __builder.AddAttribute(30, "class", "tile" + " tile-" + (
#nullable restore
#line 41 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                                           tileId

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n                                                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n");
#nullable restore
#line 43 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                            }
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                         foreach (var entity in mapService.GetEntitiesAtPosition(position, Map.Id))
                                        {
                                            switch (entity)
                                            {
                                                case Player player:

#line default
#line hidden
#nullable disable
            __builder.AddContent(33, "                                                    ");
            __builder.AddMarkupContent(34, "<div class=\"map-tile-layer\">\r\n                                                        <div class=\"tile tile-71\"></div>\r\n                                                    </div>\r\n");
#nullable restore
#line 53 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                    break;
                                                case Npc npc:

#line default
#line hidden
#nullable disable
            __builder.AddContent(35, "                                                    ");
            __builder.AddMarkupContent(36, "<div class=\"map-tile-layer\">\r\n                                                        <div class=\"tile tile-71\"></div>\r\n                                                    </div>\r\n");
#nullable restore
#line 58 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                    break;
                                                case Enemy enemy:

#line default
#line hidden
#nullable disable
            __builder.AddContent(37, "                                                    ");
            __builder.AddMarkupContent(38, "<div class=\"map-tile-layer\">\r\n                                                        <div class=\"tile tile-71\"></div>\r\n                                                    </div>\r\n");
#nullable restore
#line 63 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                    break;
                                            }
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                         if (IsTileWalkable(position))
                                        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(39, "                                            ");
            __builder.OpenElement(40, "div");
            __builder.AddAttribute(41, "class", "map-tile-layer");
            __builder.AddAttribute(42, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 68 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                                                  () => WalkTo(position)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(43, "\r\n                                                <div class=\"tile tile-hover\"></div>\r\n                                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(44, "\r\n");
#nullable restore
#line 71 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(45, "                                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(46, "\r\n");
#nullable restore
#line 73 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                }

#line default
#line hidden
#nullable disable
            __builder.AddContent(47, "                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(48, "\r\n");
#nullable restore
#line 75 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                        }
                    }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(49, "                    <div style=\"clear: both;\"></div>\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(51, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(52, "\r\n        ");
            __builder.OpenElement(53, "div");
            __builder.AddAttribute(54, "class", "panel-center");
            __builder.AddMarkupContent(55, "\r\n            ");
            __builder.OpenElement(56, "div");
            __builder.AddAttribute(57, "class", "panel-log");
            __builder.AddMarkupContent(58, "\r\n                log <br>\r\n                ");
            __builder.OpenElement(59, "input");
            __builder.AddAttribute(60, "type", "text");
            __builder.AddAttribute(61, "style", "top:-200px;position:fixed;");
            __builder.AddAttribute(62, "id", "focus");
            __builder.AddAttribute(63, "onkeydown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 84 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                                                                              HandleKeyEvent

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(64, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 84 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
                                                                             LogInput

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(65, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => LogInput = __value, LogInput));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(66, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(67, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(68, "\r\n        ");
            __builder.AddMarkupContent(69, @"<div class=""panel-right"">
            <div class=""panel-character"">
                character
            </div>
            <div class=""panel-equipment"">
                equipment
            </div>
            <div class=""panel-inventory"">
                inventory
            </div>
            <div class=""panel-quests"">
                quests
            </div>
        </div>
    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(70, "\r\n    ");
            __builder.AddMarkupContent(71, @"<div class=""panel-bottom"">
        <div class=""panel-health"">
            health
        </div>
        <div class=""panel-skillbar"">
            skillbar
        </div>
        <div class=""panel-mana"">
            mana
        </div>
    </div>
");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 114 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
       
    protected Player Player { get; set; }
    protected bool IsAdministrator { get; set; }
    protected Map Map { get; set; }
    private Timer UpdateTimer { get; set; }
    private string LogInput { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        LoadPlayerData();
        LoadMap();
        StartUpdateTimer();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        Focus("focus");
    }

    public async Task Focus(string elementId)
    {
        await JSRuntime.InvokeVoidAsync("exampleJsFunctions.focusElement", elementId);
    }

    private void LoadPlayerData()
    {
        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var username = httpContextAccessor.HttpContext.User.Identity.Name;
            Player = playerService.FindByName(username);
            IsAdministrator = httpContextAccessor.HttpContext.User.IsInRole("Administrator");
        }
    }

    private void LoadMap()
    {
        Map = mapService.Find(Player.CurrentMapId);
        if (Map == null)
        {
            ResetMap();
        }
    }

    private void ResetMap()
    {
        Map = mapService.FindByName(Rules.DefaultMapName);
        Player.CurrentMapId = Map.Id;
        Player.Position = Rules.DefaultPosition;
    }

    private void StartUpdateTimer()
    {
        UpdateTimer = new Timer();
        UpdateTimer.Interval = 200;
        UpdateTimer.AutoReset = true;
        UpdateTimer.Elapsed += UpdateTimerElapsed;
        UpdateTimer.Start();
    }

    protected void UpdateTimerElapsed(object sender, EventArgs args)
        => InvokeAsync(StateHasChanged);

    private bool IsTileWalkable((int x, int y) pos)
        => ManhattanDistance(pos.x, pos.y, Player.Position.X, Player.Position.Y) == 1
        && Map[LayerType.Solid, pos.x, pos.y] == 0;

    private int ManhattanDistance(int x1, int y1, int x2, int y2)
        => Math.Abs(x2 - x1) + Math.Abs(y2 - y1);

    private void WalkTo((int x, int y) position)
    {
        position.x -= Player.Position.X;
        position.y -= Player.Position.Y;
        if(position.x == 1) actionFactory.ExecuteMoveAction(Player, MoveAction.MoveActionDirection.Right);
        else if(position.x == -1) actionFactory.ExecuteMoveAction(Player, MoveAction.MoveActionDirection.Left);
        else if(position.y == 1) actionFactory.ExecuteMoveAction(Player, MoveAction.MoveActionDirection.Down);
        else if(position.y == -1) actionFactory.ExecuteMoveAction(Player, MoveAction.MoveActionDirection.Up);
    }

    private void HandleKeyEvent(KeyboardEventArgs args)
    {
        MoveAction.MoveActionDirection? direction = args.Key switch
        {
            "w" => MoveAction.MoveActionDirection.Up,
            "a" => MoveAction.MoveActionDirection.Left,
            "s" => MoveAction.MoveActionDirection.Down,
            "d" => MoveAction.MoveActionDirection.Right,
            _ => null
        };
        if (direction.HasValue)
        {
            actionFactory.ExecuteMoveAction(Player, direction.Value);
            LogInput = string.Empty;
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ActionFactory actionFactory { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PlayerService playerService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private MapService mapService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IHttpContextAccessor httpContextAccessor { get; set; }
    }
}
#pragma warning restore 1591
