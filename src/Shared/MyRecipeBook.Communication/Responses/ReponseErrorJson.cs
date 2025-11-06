namespace MyRecipeBook.Communication.Responses
{
    public class ReponseErrorJson
    {
        public IList<string> Errors { get; set; }
        public ReponseErrorJson(IList<string> errors) => Errors = errors;

        public ReponseErrorJson(string error) 
        { 
            Errors = new List<string>
            {
                error
            };
        }
    }
}
