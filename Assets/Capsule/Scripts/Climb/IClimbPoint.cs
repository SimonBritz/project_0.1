public interface IClimbPoint
{
    /// <summary> Сколько метров нужно до точки, чтобы начать взаимодействие </summary>
    float InteractionRadius { get; }

    /// <summary> Трансформ, куда персонаж переходит при взаимодействии </summary>
    Transform MountPoint { get; }

    /// <summary> Текст подсказки, например "E – залезть" или "X – спрыгнуть" </summary>
    string InteractionText { get; }

    /// <summary> Вызывается, когда игрок нажал кнопку взаимодействия </summary>
    void Execute(CharacterClimbController controller);
}
