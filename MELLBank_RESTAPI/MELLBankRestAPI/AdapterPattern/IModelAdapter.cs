namespace MELLBankRestAPI.AdapterPattern
{
    public interface IModelAdapter<TViewModel, TModel>
    {
        TViewModel ConvertModelToViewModel(TModel model);
        TModel ConvertViewModelToModel(TViewModel viewModel);
    }
}