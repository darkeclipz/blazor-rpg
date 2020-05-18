#pragma checksum "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f500f27ba923d835c610948a11a5520c3909113a"
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
#line 2 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

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
#line 2 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor"
using System.Timers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor"
using DungeonRpg.Engine;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor"
using DungeonRpg.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor"
using static DungeonRpg.Engine.Map;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor"
           [Authorize(Roles = "Administrator")]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/editor")]
    public partial class Editor : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 871 "C:\Users\Isomorphism\Documents\Repos\DungeonRpg\DungeonRpg\Pages\Editor.razor"
       

    private Map Map { get; set; }
    private enum DrawMode { Pen, Rectangle, All, Fill, Copy, Remove, Inspect };
    private DrawMode Mode { get; set; }
    private int SelectedTileIndex = 951;
    private Timer AutoSaveTimer;
    private bool EnableAutoSave = true;
    private LayerType Layer = 0;
    private (int X, int Y) Position = (0, 0);
    private DateTime LastSaveTime = DateTime.MinValue;
    private Timer UpdateTimer;
    private (int x, int y)? FirstSelectedPoint { get; set; }
    private (int x, int y)? InspectCoordinates { get; set; }
    private bool ShowInspectProperties { get; set; }
    private Guid InspectPropertiesSelectedEntity { get; set; }

    protected override void OnInitialized()
    {
        AutoSaveTimer = new Timer();
        AutoSaveTimer.Interval = 1000 * 60;
        AutoSaveTimer.Elapsed += AutoSave;
        AutoSaveTimer.AutoReset = true;
        AutoSaveTimer.Start();
        UpdateTimer = new Timer();
        UpdateTimer.Interval = 200;
        UpdateTimer.AutoReset = true;
        UpdateTimer.Elapsed += UpdateTimerElapsed;
        UpdateTimer.Start();
        base.OnInitialized();
    }

    private void UpdateTimerElapsed(object sender, EventArgs args)
    {
        InvokeAsync(StateHasChanged);
    }

    void OnTileChanged(int id)
    {
        // Called from child component, so we need to notify
        // the parent that the state has changed.
        SelectedTileIndex = id;
        StateHasChanged();
    }

    private void Save()
    {
        mapService.Save();
        itemService.Save();
        npcService.Save();
        enemyService.Save();
        playerService.Save();
        raceService.Save();
        LastSaveTime = DateTime.Now;
    }

    private void OpenMap(Map map)
    {
        Map = map;
        Map.UpdateProperties();
    }

    private void SetMode(DrawMode mode)
    {
        FirstSelectedPoint = null;
        Mode = mode;
    }

    private void SetLayer(LayerType layer)
    {
        Layer = layer;
    }

    private void AutoSave(object sender, EventArgs args)
    {
        if (EnableAutoSave)
        {
            Save();
        }
    }

    private void KeyboardHandler(KeyboardEventArgs args)
    {
        if (args.CtrlKey && args.Key == "s")
        {
            Save();
        }
    }

    private void Click((int x, int y) position)
    {
        switch (Mode)
        {
            case DrawMode.Pen:
                Map[Layer, position.x, position.y] = SelectedTileIndex;
                break;
            case DrawMode.Remove:
                Map[Layer, position.x, position.y] = 0;
                break;
            case DrawMode.Copy:
                SelectedTileIndex = Map[Layer, position.x, position.y];
                Mode = DrawMode.Pen;
                break;
            case DrawMode.Fill:
                Map.FloodFill(Layer, position.x, position.y, SelectedTileIndex);
                break;
            case DrawMode.All:
                Map.FillLayer(Layer, SelectedTileIndex);
                break;
            case DrawMode.Rectangle:
                if (FirstSelectedPoint == null)
                {
                    FirstSelectedPoint = position;
                }
                else if (FirstSelectedPoint == position)
                {
                    FirstSelectedPoint = null;
                }
                else
                {
                    Map.FillRectangle(Layer, FirstSelectedPoint.Value, position, SelectedTileIndex);
                    FirstSelectedPoint = null;
                }
                break;
            case DrawMode.Inspect:
                DisableAllPropertyDisplays();
                InspectCoordinates = position;
                ShowInspectProperties = true;
                break;
        }
    }

    private bool ShowItemProperties { get; set; } = false;
    private Item ItemProperties { get; set; }
    private void DisplayItemProperties(Item item)
    {
        DisableAllPropertyDisplays();
        ItemProperties = item;
        ShowItemProperties = true;
    }

    private bool ShowEntityProperties { get; set; } = false;
    private Entity EntityProperties { get; set; }
    private void DisplayEntityProperties(Entity entity)
    {
        DisableAllPropertyDisplays();
        EntityProperties = entity;
        ShowEntityProperties = true;
    }

    private bool ShowRaceProperties { get; set; } = false;
    private Race RaceProperties { get; set; }
    private void DisplayRaceProperties(Race race)
    {
        DisableAllPropertyDisplays();
        RaceProperties = race;
        ShowRaceProperties = true;
    }

    private void DisableAllPropertyDisplays()
    {
        ShowItemProperties = false;
        ShowEntityProperties = false;
        ShowInspectProperties = false;
        ShowRaceProperties = false;
        InspectCoordinates = null;
    }

    private void Move(int x, int y)
    {
        Position.X += x;
        Position.Y += y;
        StateHasChanged();
    }

    private IEnumerable<Entity> GetEntities()
    {
        var npcs = npcService.All().Select(npc => npc as Entity);
        var enemies = enemyService.All().Select(enemy => enemy as Entity);
        var entities = new List<Entity>();
        entities.AddRange(npcs);
        entities.AddRange(enemies);
        return entities;
    }

    private void AddEntityToMap(Guid entityId, (int x, int y) position)
    {
        var entity = npcService.All().FirstOrDefault(npc => npc.Id == entityId) as Entity;
        if (entity == null)
        {
            entity = enemyService.All().FirstOrDefault(enemy => enemy.Id == entityId) as Entity;
        }
        if (entity != null)
        {
            Map.AddEntity(entity, position);
        }
    }

    private bool ShowInspectOverlay((int x, int y) position)
        => (!Map.IsEmpty(position) || playerService.GetPlayersAtPosition(position, Map.Id).Count() > 0)
        && Mode == DrawMode.Inspect;

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private RaceService raceService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PlayerService playerService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private EnemyService enemyService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NpcService npcService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ItemService itemService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ComponentService componentService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private MapService mapService { get; set; }
    }
}
#pragma warning restore 1591
