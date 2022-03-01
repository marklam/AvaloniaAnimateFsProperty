namespace ControlLibraryFS

open System
open Avalonia
open Avalonia.Controls

type StringFormatFloatTextBlock() =
    inherit TextBlock()
    static let StringFormatProperty = AvaloniaProperty.Register<StringFormatFloatTextBlock, string>("StringFormat", "")
    static let ValueProperty        = AvaloniaProperty.Register<StringFormatFloatTextBlock, double>("Value")

    static let updateValue (e : AvaloniaPropertyChangedEventArgs<double>) =
        match e.Sender with
        | :? StringFormatFloatTextBlock as sender ->
            sender.Text <- String.Format(sender.StringFormat, e.NewValue.Value)
        | _ -> ()

    static let updateStringFormat (e : AvaloniaPropertyChangedEventArgs<string>) =
        match e.Sender with
        | :? StringFormatFloatTextBlock as sender ->
            sender.Text <- String.Format(e.NewValue.Value, sender.Value)
        | _ -> ()

    static do
        StringFormatFloatTextBlock.RegisterRenderDependenicies()
        StringFormatProperty.Changed |> Observable.subscribe updateStringFormat |> ignore
        ValueProperty.Changed        |> Observable.subscribe updateValue        |> ignore

    static member RegisterRenderDependenicies() =
        Visual.AffectsRender<StringFormatFloatTextBlock>(
            StringFormatProperty,
            ValueProperty
        )

    member this.StringFormat
        with get () = this.GetValue(StringFormatProperty)
        and  set v  = this.SetValue(StringFormatProperty, v) |> ignore

    member this.Value
        with get () : double  = this.GetValue(ValueProperty)
        and  set (v : double) = this.SetValue(ValueProperty, v) |> ignore

