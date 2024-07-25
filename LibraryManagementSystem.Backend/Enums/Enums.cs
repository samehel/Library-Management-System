namespace LibraryManagementSystem.Backend.Enums
{
    public enum TransactionType
    {
        BORROW,
        RETURN
    }

    public enum PaymentStatus
    {
        PAID,
        NOT_PAID
    }

    public enum ActionType
    {
        ADD_BOOK,
        REMOVE_BOOK,
        WARN_USER,
        BORROW_BOOK,
        RETURN_BOOK,
        LOGIN,
        LOGOUT,
        REGISTER,
        GET_ALL_USERS,
        UPDATE_USER,
        DELETE_USER,
        GET_BOOK,
        UPDATE_BOOK,
        CREATE_BOOK,
        DELETE_BOOK
    }

    public enum Genre
    {
        Science,
        Literature,
        History,
        Technology,
        Art,
        Music,
        Philosophy,
        Religion,
        SocialScience,
        Language,
        Mathematics,
        Medicine,
        Law,
        Education,
        Psychology,
        Fiction,
        Poetry,
        Drama,
        Travel,
        Biography,
        Business,
        SelfHelp,
        Health,
        ScienceFiction,
        Mystery
    }
}
