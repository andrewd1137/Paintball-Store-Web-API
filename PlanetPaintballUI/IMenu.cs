namespace PPUI
{

    public interface IMenu
    {

        /// <summary>
        /// will display the menu and user choices in terminal
        /// </summary>
        void Display();

        /// <summary>
        /// Will record the users choice and change/route menu based on that choice
        /// </summary>
        /// <returns>return the menu that will change your screen</returns>
        string UserChoice();

    }

}