#pragma checksum "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b7a3cde5813d3a4080b77e745bd8bb134028b027"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
using DungeonRpg.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using System.Timers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
using static DungeonRpg.Engine.Map;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
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
        }
        #pragma warning restore 1998
#nullable restore
#line 115 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Client.razor"
       
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
        && Map.IsWalkable(pos);

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
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ActionProvider actionFactory { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PlayerService playerService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private MapService mapService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IHttpContextAccessor httpContextAccessor { get; set; }
    }
}
#pragma warning restore 1591
