using UnityEngine;

namespace MiniLocale.Components
{
	/// <summary>
	/// Base component to inherit from for components that will localize a text component type
	/// </summary>
	public abstract class LocalizeComponent : MonoBehaviour
	{
		/// <summary>
		/// Tag to localize the text with
		/// </summary>
		[SerializeField]
		protected string textTag;

		/// <summary>
		/// Has the object been subscribed to localizer events?
		/// </summary>
		private bool _subscribed = false;

		/// <summary>
		/// Public access to the text tag if required
		/// </summary>
		public string Tag => textTag;

		/// <summary>
		/// Set the text of this object to the given string
		/// Is called on start and if the language source changes in localizer
		/// </summary>
		/// <param name="text">String to set the text to</param>
		public abstract void SetText(string text);

		/// <summary>
		/// Init will get called at start before any localize calls
		/// </summary>
		protected virtual void Init()
		{ }

		/// <summary>
		/// Localize the object
		/// </summary>
		public void Localize()
		{
			SetText(Localizer.GetString(Tag));
		}

		/// <summary>
		/// Localize the text and subscribe to events when object starts
		/// </summary>
		private void Start()
		{
			Init();
			Localize();
			Subscribe();
		}

		/// <summary>
		/// Unsubscribe from events when object in destroyed
		/// </summary>
		private void OnDestroy()
		{
			Unsubscribe();
		}

		/// <summary>
		/// Subscribe to localizer events
		/// </summary>
		private void Subscribe()
		{
			if (_subscribed) return;
			Localizer.SourceChanged += Localize;
			_subscribed = true;
		}

		/// <summary>
		/// Unsubscribe from localizer events
		/// </summary>
		private void Unsubscribe()
		{
			if (!_subscribed) return;
			Localizer.SourceChanged -= Localize;
			_subscribed = false;
		}
	}
}