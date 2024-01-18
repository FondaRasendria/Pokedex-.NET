using System.Net;

namespace Pokedex
{
    public static class ImageChecker
    {
        public static bool checkIfImageExists(string imageUrl)
        {
            try
            {
                WebRequest request = WebRequest.Create(imageUrl);

                using (WebResponse response = request.GetResponse())
                {
                    if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (WebException ex)
            {
            }
            return false;
        }
    }
}
