스프라이트는 픽셀로 만들어져 있고
해상도는 픽셀의 갯수
각 픽셀은 정보를 담고있고

유니티는 유닛을 가지고있음

어느 단위든 상관없음

카메라 따라가기 

언제 시작할지 정하지 않으면 카메라가 흔들리는 문제가 발생한다
.
함수 실행 순서
Awake: 이 함수는 항상 Start 함수 전에 호출되며 프리팹이 인스턴스화 된 직후에 호출됩니다. 게임 오브젝트가 시작하는 동안 비활성 상태인 경우 Awake 함수는 활성화될 때까지 호출되지 않습니다.
OnEnable: (오브젝트가 활성화된 경우에만): 오브젝트 활성화 직후 이 함수를 호출합니다. 레벨이 로드되거나 스크립트 컴포넌트를 포함한 게임 오브젝트가 인스턴스화될 때와 같이 MonoBehaviour를 생성할 때 이렇게 할 수 있습니다.
씬에 추가된 모든 오브젝트에 대해 Start, Update 등 이전에 호출된 모든 스크립트를 위한 Awake 및 OnEnable 함수가 호출됩니다. 따라서 게임플레이 도중 오브젝트를 인스턴스화될 때는 실행되지 않습니다.


에디터
Reset: 오브젝트에 처음 연결하거나 Reset 커맨드를 사용할 때 스크립트의 프로퍼티를 초기화하기 위해 Reset을 호출합니다.
OnValidate: OnValidate는 오브젝트가 역직렬화될 때를 포함하여 스크립트의 프로퍼티가 설정될 때마다 호출되며, 이는 에디터에서 씬을 열 때와 도메인을 다시 로드한 후와 같이 다양한 시기에 발생할 수 있습니다.

첫 번째 프레임 업데이트 전에
Start: 스크립트 인스턴스가 활성화된 경우에만 첫 번째 프레임 업데이트 전에 호출됩니다.
씬 에셋에 포함된 모든 오브젝트에 대해 Update 등 이전에 호출된 모든 스크립트를 위한 Start 함수가 호출됩니다. 따라서 게임플레이 도중 오브젝트를 인스턴스화될 때는 실행되지 않습니다.


프레임 사이
OnApplicationPause: 이 함수는 일시 정지가 감지된 프레임의 끝, 실질적으로는 일반 프레임 업데이트 사이에 호출됩니다. 게임에 일시정지 상태를 가리키는 그래픽스를 표시하도록 OnApplicationPause 가 호출된 후에 한 프레임이 추가로 실행됩니다.

업데이트 순서
게임 로직, 상호작용, 애니메이션, 카메라 포지션의 트랙을 유지할 때, 사용 가능한 몇몇 다른 이벤트가 존재합니다. 일반적인 패턴은 Update 함수에 대부분의 작업을 수행하는 것이지만, 사용할 수 있는 다른 함수도 있습니다.

FixedUpdate: FixedUpdate 는 종종 Update 보다 더 자주 호출됩니다. 프레임 속도가 낮은 경우 프레임당 여러 번 호출될 수 있으며 프레임 속도가 높은 경우 프레임 사이에 호출되지 않을 수 있습니다. 모든 물리 계산 및 업데이트는 FixedUpdate 후 즉시 발생합니다. FixedUpdate 의 움직임 계산을 적용할 때 Time.deltaTime 만큼 값을 곱할 필요가 없습니다. FixedUpdate 가 프레임 속도와 관계없이 신뢰할 수있는 타이머에서 호출되기 때문입니다.

Update: Update 는 프레임당 한 번 호출됩니다. 프레임 업데이트를 위한 주요 작업 함수입니다.

LateUpdate: LateUpdate 는 Update 가 끝난 후 프레임당 한 번 호출됩니다. Update 에서 수행된 모든 계산은 LateUpdate 가 시작할 때 완료됩니다. LateUpdate 는 일반적으로 다음의 3인칭 카메라에 사용합니다. 캐릭터를 움직이고 Update 로 방향을 바꾸게 하는 경우 LateUpdate 에서 모든 카메라 움직임과 로테이션 계산을 수행할 수 있습니다. 이렇게 하면 카메라가 포지션을 추적하기 전에 캐릭터가 완전히 움직였는지 확인할 수 있습니다.

일반적으로 순서가 명시적으로 문서화되거나 설정 가능한 경우를 제외하고, 다른 게임 오브젝트에 대해 동일한 이벤트 함수가 호출되는 순서를 사용해서는 안 됩니다. (플레이어 루프를 보다 세세하게 제어해야 하는 경우 PlayerLoop API를 사용할 수 있습니다.)

동일한 MonoBehaviour 서브 클래스의 다른 인스턴스에 대해 이벤트 함수가 호출되는 순서를 지정할 수 없습니다. 예를 들어 한 MonoBehaviour의 Update 함수는 다른 게임 오브젝트(해당 부모 또는 자식 게임 오브젝트 포함)의 동일한 MonoBehaviour에 대해 Update 함수 전후에 호출될 수 있습니다.

한 MonoBehaviour 서브 클래스의 이벤트 함수가 다른 서브 클래스의 이벤트 함수 전에 호출되도록 지정할 수 있습니다(Project Settings 창의 Script Execution Order 패널 사용). 예를 들어 두 개의 스크립트(EngineBehaviour, SteeringBehaviour)가 있는 경우 EngineBehaviour가 항상 SteeringBehaviour 전에 업데이트되도록 스크립트 실행 순서를 설정할 수 있습니다.


애니메이션 업데이트 루프
Unity가 애니메이션 시스템을 평가할 때 이러한 함수와 프로파일러 마커가 호출됩니다.

OnStateMachineEnter: State Machine Update 단계 동안 컨트롤러의 상태 머신이 엔트리 상태를 통과하는 전환을 만들 때 이 콜백이 첫 번째 업데이트 프레임에 대해 호출됩니다. StateMachine 하위 상태에 대한 전환의 경우에는 호출되지 않습니다.

이 콜백은 Controller 컴포넌트(예: AnimatorController 또는 AnimatorOverrideController 또는 AnimatorControllerPlayable)가 애니메이션 그래프에 있을 때에만 발생합니다.

참고: 이 콜백을 StateMachineBehaviour 컴포넌트에 추가하면 멀티스레드 상태 머신 평가가 비활성화됩니다.

OnStateMachineExit: State Machine Update 단계 동안 컨트롤러의 상태 머신이 종료 상태를 통과하는 전환을 만들 때 이 콜백이 마지막 업데이트 프레임에 대해 호출됩니다. StateMachine 하위 상태에 대한 전환의 경우에는 호출되지 않습니다.

이 콜백은 Controller 컴포넌트(예: AnimaorController 또는 AnimatorOverrideController 또는 AnimatorControllerPlayable)가 애니메이션 그래프에 있을 때에만 발생합니다.

참고: 이 콜백을 StateMachineBehaviour 컴포넌트에 추가하면 멀티스레드 상태 머신 평가가 비활성화됩니다.

Fire Animation Events: 마지막 업데이트 시간과 최신 업데이트 시간 사이에 샘플링된 모든 클립에서 모든 애니메이션 이벤트를 호출합니다.

StateMachineBehaviour (OnStateEnter/OnStateUpdate/OnStateExit): 레이어가 최대 3개의 활성 상태(현재 상태, 중단된 상태, 다음 상태)를 가질 수 있습니다. 이 함수는 OnStateEnter, OnStateUpdate 또는 OnStateExit 콜백을 정의하는 StateMachineBehaviour 컴포넌트가 포함된 각 활성 상태에 대해 호출됩니다.

제일 먼저 함수가 현재 상태에 대해 호출되고 중단된 상태에 대해 호출된 후 마지막으로 다음 상태에 대해 호출됩니다.

이 단계는 Controller 컴포넌트(예: AnimatorController 또는 AnimatorOverrideController 또는 AnimatorControllerPlayable)가 애니메이션 그래프에 있을 때에만 발생합니다.

OnAnimatorMove: 업데이트 프레임마다 루트 모션을 수정할 수 있도록 각 Animator 컴포넌트에 대해 한 번 호출됩니다.

StateMachineBehaviour(OnStateMove): 이 콜백을 정의하는 StateMachineBehaviour가 포함된 각 활성 상태에 대해 호출됩니다.

OnAnimatorIK: 애니메이션 IK를 설정합니다. IK pass가 활성화된 각 애니메이터 컨트롤러 레이어에 대해 한 번 호출됩니다.

휴머노이드 릭을 사용하는 경우에만 이 이벤트가 실행됩니다.

StateMachineBehaviour(OnStateIK): IK pass가 활성화되어 있는 레이어에서 이 콜백을 정의하는 StateMachineBehaviour 컴포넌트가 포함된 각 활성 상태에 대해 호출됩니다.

WriteProperties: 다른 모든 애니메이션화된 프로퍼티를 메인 스레드에서 씬에 작성합니다.

유용한 프로파일 마커
스크립트 라이프사이클 플로우차트에 표시된 일부 애니메이션 함수는 호출할 수 없는 이벤트 함수입니다. 즉, Unity가 애니메이션을 처리할 때 호출되는 내부 함수입니다.

이 함수에는 프로파일 마커가 있으므로 프로파일러를 사용하여 프레임에서 Unity가 호출하는 시간을 확인할 수 있습니다. Unity가 이러한 함수를 호출하는 시간을 알면 호출한 이벤트 함수가 실행된 시간을 정확하게 파악하는 데 도움이 됩니다.

예를 들어 FireAnimationEvents 콜백에서 Animator.Play를 호출한다고 가정해 보겠습니다. State Machine Update 및 Process Graph 함수가 실행된 후에 FireAnimationEvents 콜백이 발동했다는 사실을 알면 애니메이션 클립이 즉시 재생되는 것이 아니라 다음 프레임에서 재생된다는 것을 예측할 수 있습니다.

State Machine Update: 이 단계에서 모든 상태 머신이 실행 시퀀스대로 평가됩니다. 이 단계는 Controller 컴포넌트(예: AnimatorController 또는 AnimatorOverrideController 또는 AnimatorControllerPlayable)가 애니메이션 그래프에 있을 때에만 발생합니다.

참고: 상태 머신 평가는 대개 멀티스레드이지만, 특정 콜백(예: OnStateMachineEnter 및 OnStateMachineExit)을 추가하면 멀티스레딩이 비활성화됩니다. 자세한 내용은 위의 애니메이션 업데이트 루프를 참조하십시오.

ProcessGraph: 모든 애니메이션 그래프를 평가합니다. 여기에는 평가할 모든 애니메이션 클립에 대한 샘플링과 루트 모션 계산이 포함됩니다.

ProcessAnimation: 애니메이션 그래프의 결과를 블렌딩합니다.

WriteTransforms: 모든 애니메이션화된 트랜스폼을 워커 스레드에서 씬에 작성합니다.

IK pass가 활성화된 여러 개의 레이어가 있는 휴머노이드 릭은 여러 개의 WriteTransforms 패스를 가질 수 있습니다(스크립트 라이프사이클 플로우차트 참조).


렌더링
OnPreCull: 카메라가 씬을 컬링하기 전에 호출됩니다. 컬링은 어떤 오브젝트를 카메라에 표시할지 결정합니다. OnPreCull은 컬링 발생 직전에 호출됩니다.
OnBecameVisible/OnBecameInvisible: 오브젝트가 카메라에 표시되거나/표시되지 않을 때 호출됩니다.
OnWillRenderObject: 오브젝트가 표시되면 각 카메라에 한 번 호출됩니다.
OnPreRender: 카메라가 씬 렌더링을 시작하기 전에 호출됩니다.
OnRenderObject: 모든 일반 씬 렌더링이 처리된 후 호출됩니다. 이 때 커스텀 지오메트리를 그리는 데에 GL 클래스 또는 Graphics.DrawMeshNow를 사용할 수 있습니다.
OnPostRender: 카메라가 씬 렌더링을 마친 후 호출됩니다.
OnRenderImage: 씬 렌더링이 완료된 후 호출되어 이미지의 포스트 프로세싱이 가능합니다. 포스트 프로세싱 효과를 참고하십시오.
OnGUI: GUI 이벤트에 따라 프레임당 여러 번 호출됩니다. 레이아웃 및 리페인트 이벤트는 우선 처리되며 각 입력 이벤트에 대해 레이아웃 및 키보드/마우스 이벤트가 다음으로 처리됩니다.
OnDrawGizmos: 시각화 목적으로 씬 뷰에 기즈모를 그릴 때 사용됩니다.
참고: 이러한 콜백은 빌트인 렌더 파이프라인에서만 작동합니다.


코루틴
일반적인 코루틴 업데이트는 Update 함수가 반환된 후 실행됩니다. 코루틴은 주어진 YieldInstruction이 완료될 때까지 실행을 중단(양보)할 수 있는 함수입니다. 코루틴의 다른 사용법은 다음과 같습니다.

yield 코루틴은 모든 Update 함수가 다음 프레임에 호출된 후 계속됩니다.
yield WaitForSeconds 지정한 시간이 지난 후, 모든 Update 함수가 프레임에 호출된 후 계속됩니다.
yield WaitForFixedUpdate 모든 FixedUpdate가 모든 스크립트에 호출된 후 계속됩니다. FixedUpdate 전에 코루틴이 양보하면 현재 프레임의 FixedUpdate 이후에 재개합니다.
yield WWW WWW 다운로드가 완료된 후 계속됩니다.
yield StartCoroutine 코루틴을 연결하고 MyFunc 코루틴이 먼저 완료되기를 기다립니다.

오브젝트를 파괴할 때
OnDestroy: 오브젝트 존재의 마지막 프레임에 대해 모든 프레임 업데이트를 마친 후 이 함수가 호출됩니다. 오브젝트는 Object.Destroy 또는 씬 종료에 대한 응답으로 파괴될 수 있습니다.

종료할 때
다음 함수는 씬의 활성화된 모든 오브젝트에서 호출됩니다.

OnApplicationQuit: 이 함수는 애플리케이션 종료 전 모든 게임 오브젝트에서 호출됩니다. 에디터에서 사용자가 플레이 모드를 중지할 때 호출됩니다.
OnDisable: 동작이 비활성화되거나 비활성 상태일 때 이 함수가 호출됩니다.

Destroy(): 패키지 삭제
SerializeField를 통해 사라질때 딜레이 조절가능 
