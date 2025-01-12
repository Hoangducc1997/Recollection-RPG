using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socials : MonoBehaviour
{
    [SerializeField] private string facebookURL = "https://www.facebook.com/profile.php?id=61570034045910";
    [SerializeField] private string itchIoURL = "https://itch.io/dashboard";

    public void OpenFacebook()
    {
        // Mở trình duyệt và điều hướng đến trang Facebook
        Application.OpenURL(facebookURL);
    }

    public void OpenItchIo()
    {
        // Mở trình duyệt và điều hướng đến trang itch.io
        Application.OpenURL(itchIoURL);
    }
}
