using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using static AzureShowcase.Shared.DataClasses;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;

namespace AzureShowcase
{
    public class FunctionCaller : MonoBehaviour
    {
        [SerializeField] bool localHost = false;

        const string MAIN_URL = "https://errolazureshowcase.azurewebsites.net/api/adddata";
        const string TEST_URL = "http://localhost:7017/api/AddData";

        public string URL => localHost ? TEST_URL : MAIN_URL;

        static readonly HttpClient client = new HttpClient();

        void Start()
        {
            StartCoroutine(AddData());
        }

        IEnumerator AddData()
        {
            WWWForm form = new WWWForm();
            form.AddField("name", "errol");

            using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }
    }
}
