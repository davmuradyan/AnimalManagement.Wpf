using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AnimalManagement.Wpf.Infrastructure.Behaviors;

public static class PasswordBoxHelper
{
    public static readonly DependencyProperty BoundPasswordProperty =
        DependencyProperty.RegisterAttached(
            "BoundPassword",
            typeof(string),
            typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnBoundPasswordChanged));

    public static string GetBoundPassword(DependencyObject obj) =>
        obj.GetValue(BoundPasswordProperty) as string ?? string.Empty;

    public static void SetBoundPassword(DependencyObject obj, string value) =>
        obj.SetValue(BoundPasswordProperty, value);

    private static readonly DependencyProperty IsUpdatingProperty =
        DependencyProperty.RegisterAttached(
            "IsUpdating",
            typeof(bool),
            typeof(PasswordBoxHelper),
            new PropertyMetadata(false));

    private static bool GetIsUpdating(DependencyObject obj) =>
        (bool)obj.GetValue(IsUpdatingProperty);

    private static void SetIsUpdating(DependencyObject obj, bool value) =>
        obj.SetValue(IsUpdatingProperty, value);

    private static void OnBoundPasswordChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox)
            return;

        passwordBox.PasswordChanged -= PasswordChanged;

        if (!GetIsUpdating(passwordBox))
            passwordBox.Password = e.NewValue as string ?? string.Empty;

        passwordBox.PasswordChanged += PasswordChanged;
    }

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox passwordBox)
            return;

        SetIsUpdating(passwordBox, true);

        passwordBox.SetCurrentValue(
            BoundPasswordProperty,
            passwordBox.Password);

        BindingOperations
            .GetBindingExpression(passwordBox, BoundPasswordProperty)
            ?.UpdateSource();

        SetIsUpdating(passwordBox, false);
    }
}