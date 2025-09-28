namespace TQVaultAE.IO
{
    internal class TitanQuestFileService
    {
        // TODO Replace mapper
        //private IMapper _mapper = mapper;
        //internal TitanQuestFileService(IMapper mapper) => _mapper = mapper;

        internal TitanQuestFileService() { }


        public TitanQuestFile ReadFile(string path)
		{
			//TitanQuestFile file = TitanQuestFile.ReadFile(path, _mapper);
			TitanQuestFile file = TitanQuestFile.ReadFile(path);
			file.Parse();
			file.Analyse();
			return file;
		}
    }
}
