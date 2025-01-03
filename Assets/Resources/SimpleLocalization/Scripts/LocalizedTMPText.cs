using UnityEngine;
using TMPro;

namespace Assets.SimpleLocalization.Scripts
{
    /// <summary>
    /// Localize TMP_Text component.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedTMPText : MonoBehaviour
    {
        public string LocalizationKey;

        private TMP_Text _textComponent;

        private void Awake()
        {
            // Lấy component TMP_Text khi script được khởi tạo
            _textComponent = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            // Localize khi script được khởi động
            Localize();

            // Lắng nghe sự kiện thay đổi ngôn ngữ
            LocalizationManager.OnLocalizationChanged += Localize;
        }

        private void OnDestroy()
        {
            // Gỡ lắng nghe sự kiện để tránh lỗi
            LocalizationManager.OnLocalizationChanged -= Localize;
        }

        public void Localize()
        {
            if (_textComponent == null)
            {
                Debug.LogWarning($"No TMP_Text component found on {gameObject.name}");
                return;
            }

            // Cập nhật text từ LocalizationManager
            _textComponent.text = LocalizationManager.Localize(LocalizationKey);
        }
    }
}
