using UnityEngine;

namespace MiniLocale.Components
{
	/// <summary>
	/// Base component to inherit from for components that will localize a text component type
	/// </summary>
	public abstract class LocalizeComponent<T> : MonoBehaviour where T : MonoBehaviour
	{
		/// <summary>
		/// Target object to localize
		/// </summary>
		[SerializeField]
		protected T target;
		
		/// <summary>
		/// Tag to localize the text with
		/// </summary>
		[SerializeField]
		protected string textTag;

		/// <summary>
		/// Has the object been subscribed to localizer events?
		/// </summary>
		private bool subscribed = false;

		/// <summary>
		/// Public access to the text tag if required
		/// </summary>
		public string Tag
		{
			get => textTag;
			set
			{
				textTag = value;
				if (subscribed) Localize();
			}
		}

		/// <summary>
		/// Set the text of this object to the given string
		/// Is called on start and if the language source changes in localizer, or the tag is changed
		/// </summary>
		/// <param name="text">String to set the text to</param>
		protected abstract void SetText(string text);

		/// <summary>
		/// Get the currently displayed text on the target
		/// </summary>
		/// <returns>The currently displayed text</returns>
		public abstract string GetDisplayedText();

		/// <summary>
		/// Init will get called at start before any localize calls
		/// </summary>
		protected virtual void Init()
		{ }

		/// <summary>
		/// Localize the object
		/// </summary>
		private void Localize()
		{
			SetText(Localizer.GetString(Tag));
		}

		/// <summary>
		/// Localize the text and subscribe to events when object starts
		/// </summary>
		protected virtual void Start()
		{
			Init();
			Localize();
			Subscribe();
		}

		/// <summary>
		/// Unsubscribe from events when object in destroyed
		/// </summary>
		protected virtual void OnDestroy()
		{
			Unsubscribe();
		}

		protected virtual void Reset()
		{
			if (target == null) target = GetComponent<T>();
		}

		/// <summary>
		/// Subscribe to localizer events
		/// </summary>
		private void Subscribe()
		{
			if (subscribed) return;
			Localizer.SourceChanged += Localize;
			subscribed = true;
		}

		/// <summary>
		/// Unsubscribe from localizer events
		/// </summary>
		private void Unsubscribe()
		{
			if (!subscribed) return;
			Localizer.SourceChanged -= Localize;
			subscribed = false;
		}
	}
}