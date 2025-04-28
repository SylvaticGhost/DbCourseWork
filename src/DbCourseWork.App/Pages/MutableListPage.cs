using Ardalis.GuardClauses;
using Ardalis.Result;
using Core.Interfaces;
using Microsoft.AspNetCore.Components;
using Utils.Extensions;
using WebUI.Components.Elements;
using WebUI.Utils;

namespace WebUI.Pages;

public abstract class MutableListPage<TEntity, TCreateDto, TParam> : ComponentBase
    where TEntity : class, IDbEntity, IMutableFormEntity<TCreateDto, TParam>
    where TCreateDto : class
    where TParam : IUpsertParam<TCreateDto>
{
    protected readonly PageError PageError = new();
    
    protected TableEntity<TEntity>? TableEntity;
    
    protected TEntity? SelectedEntity;

    protected Modal? ModalRef;

    protected abstract TParam UpsParam { get; set; }
    
    protected Task InvokeUpdateModel(TEntity entity)
    {
        SelectedEntity = entity; 
        UpsParam = entity.ToUpsertParam();
        Guard.Against.Null(ModalRef);
        return ModalRef.Show() ?? Task.CompletedTask;
    }
    
    protected async Task ContentChange(Func<TCreateDto, Task<Result<TEntity>>> func, TParam param) 
    {
        var dto = param.ToCreateDto();
        var result = await func(dto);
        if (!result.IsSuccess)
        {
            Console.WriteLine(result.JoinErrorMessage());
            PageError.SetOnFailure(result);
            return;
        }
        ClearForm();
        if(TableEntity is not null)
            await TableEntity.ReloadData();
    }

    protected virtual void ClearForm() => UpsParam.Clear();
}