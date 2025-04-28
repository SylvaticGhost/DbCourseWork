using Ardalis.GuardClauses;
using Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace WebUI.Components.Elements.Forms;

public abstract class UpsertFormBase<TEntity,TCreateDto,TParam> : ComponentBase
    where TEntity : class, IDbEntity, IMutableFormEntity<TCreateDto, TParam>
    where TCreateDto : class
    where TParam : IUpsertParam<TCreateDto>
{
    protected static readonly IFluentColumnWithSize ColumnSize = Blazorise.ColumnSize.Is6.OnDesktop.Is12.OnMobile;
    
    [Parameter]
    public required TParam Param { get; init; }
    
    [Parameter]
    public TEntity? Source { get; init; }
    
    [Parameter]
    public Func<TParam ,Task>? Create { get; init; }
    
    [Parameter]
    public Func<TEntity, TParam, Task>? Update { get; init; }

    protected Task UpdateClick()
    {
        Guard.Against.Null(Source);
        Guard.Against.Null(Update);
        return Update(Source, Param);
    }
    
    protected Task CreateClick()
    {
        Guard.Against.Null(Create);
        return Create(Param);
    }
}