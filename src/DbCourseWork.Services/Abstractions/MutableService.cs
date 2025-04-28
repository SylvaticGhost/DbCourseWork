using Ardalis.Result;
using Core.Interfaces;
using Data.Abstracrtions;
using FluentValidation;
using Utils;
using ResultExtensions = Utils.Extensions.ResultExtensions;

namespace Services.Abstractions;

public abstract class MutableService<TEntity, TCreateDto, TCreateValidator>(IRepository<TEntity> repository)
    : SearchableService<TEntity>(repository), IMutableService<TEntity, TCreateDto>
    where TEntity : class, IDbEntity, IFormTableEntity
    where TCreateDto : class
    where TCreateValidator : AbstractValidator<TCreateDto>, new()
{
    public Task<Result> Delete(TEntity entity) => ResultExtensions.InErrorHandler(repository.Delete(entity));

    protected abstract TEntity FabricMethod(TCreateDto createDto);

    protected abstract Task<Result<TEntity>> CreateChecks(TEntity entity);

    private Result<TEntity> CreateEntity(TCreateDto createDto)
    {
        var validation = Validator.Use<TCreateValidator, TCreateDto>(createDto);

        if (!validation.IsSuccess)
            return validation;

        return FabricMethod(createDto);
    }

    public async Task<Result<TEntity>> Create(TCreateDto createDto)
    {
        Result<TEntity> entityResult = CreateEntity(createDto);

        if (!entityResult.IsSuccess)
            return entityResult;

        Result<TEntity> checksResult = await CreateChecks(entityResult.Value);
        if (!checksResult.IsSuccess)
            return checksResult;

        return await ResultExtensions.InErrorHandler(repository.Insert(entityResult), checksResult);
    }

    public Task<Result<TEntity>> Update(TEntity entity, TCreateDto dto)
    {
        Result<TEntity> newRecord = CreateEntity(dto);
        return !newRecord.IsSuccess
            ? Task.FromResult(newRecord)
            : ResultExtensions.InErrorHandler(repository.Update(entity, newRecord.Value), newRecord);
    }
}