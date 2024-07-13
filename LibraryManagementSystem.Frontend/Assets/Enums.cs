﻿namespace LibraryManagementSystem.Frontend.Enums
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
        REGISTER
    }
}
